using ImageApp.Data;
using ImageApp.Models;
using ImageApp.Services.ImageGeneration;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ImageApp.Services.Scheduling
{
    public class DailyImageService : HostedService
    {
        /// <summary>
        /// Hour on which we run the task
        /// </summary>
        private const int _hourToRun = 0;

        /// <summary>
        /// Did we run the task yet during the current hour?
        /// </summary>
        private bool _ranThisHour = false;

        /// <summary>
        /// How many images should we generate and store daily?
        /// </summary>
        private const int _numberOfImagesToGenerate = 10;

        private readonly ApplicationDbContext _context;
        private readonly ImageHalfGenerator _imageHalfGenerator;

        public DailyImageService(ApplicationDbContext context, ImageHalfGenerator imageHalfGenerator)
        {
            _context = context;
            _imageHalfGenerator = imageHalfGenerator;
        }

        /// <summary>
        /// Generates and stores image halves once per day
        /// </summary>
        public override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                int hour = DateTime.UtcNow.Hour;

                // it's the correct hour and we haven't run the task yet
                if (hour == _hourToRun && !_ranThisHour)
                {
                    await GenerateAndStoreImageHalves(_numberOfImagesToGenerate);
                    _ranThisHour = true;
                }
                // it's not the correct hour anymore, we need to reset our flag
                else if (hour != _hourToRun && _ranThisHour)
                {
                    _ranThisHour = false;
                }

                // check again in a minute
                await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
            }
        }

        /// <summary>
        /// Uses <see cref="_imageHalfGenerator"/> to generate the specified number of image halves
        /// and stores them in the database
        /// </summary>
        /// <param name="count">Number of image halves to generate</param>
        public async Task GenerateAndStoreImageHalves(int count)
        {
            var imageHalves = new List<ImageHalf>();

            for (int x = 0; x < count; x++)
            {
                imageHalves.Add(_imageHalfGenerator.Generate());
            }

            _context.ImageHalves.AddRange(imageHalves);

            await _context.SaveChangesAsync();
        }
    }
}
