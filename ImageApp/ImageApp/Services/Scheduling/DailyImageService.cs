using ImageApp.Data;
using ImageApp.Models;
using ImageApp.Services.ImageGeneration;
using ImageApp.Services.Logging;
using ImageApp.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ImageApp.Services.Scheduling
{
    /// <summary>
    /// Generates new image grids on a set schedule once per day
    /// </summary>
    public class DailyImageService : HostedService
    {
        /// <summary>
        /// Hour on which we run the task (0 to 23)
        /// </summary>
        public int HourToRun { get; }

        /// <summary>
        /// Did we run the task yet during the current hour?
        /// </summary>
        public bool RanThisHour { get; private set; } = false;

        /// <summary>
        /// How many images should we generate and store daily?
        /// </summary>
        public const int _numberOfImagesToGenerate = 10;

        /// <summary>
        /// Path to the folder where image half json files are stored in wwwroot
        /// </summary>
        public readonly string FolderPath;

        private readonly IServiceProvider _serviceProvider;
        private readonly IHostingEnvironment _hostingEnvironment;

        public DailyImageService(IServiceProvider serviceProvider,
            IHostingEnvironment hostingEnvironment, int hourToRun = 0)
        {
            _serviceProvider = serviceProvider;
            _hostingEnvironment = hostingEnvironment;
            HourToRun = hourToRun;

            FolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "images/daily");
        }

        /// <summary>
        /// Generates and stores image grids once per day
        /// </summary>
        public override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                int hour = DateTime.UtcNow.Hour;

                // it's the correct hour and we haven't run the task yet
                if (hour == HourToRun && !RanThisHour)
                {
                    RanThisHour = await GenerateAndStoreImageGrids(_numberOfImagesToGenerate);
                }
                // it's not the correct hour anymore, we need to reset our flag
                else if (hour != HourToRun && RanThisHour)
                {
                    RanThisHour = false;
                }

                // check again in a minute
                await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
            }
        }

        /// <summary>
        /// Uses <see cref="ImageHalfGenerator.Generate"/> to generate the specified number of image grids
        /// and stores them in the database
        /// </summary>
        /// <param name="count">Number of image grids to generate</param>
        /// <returns>true if successful</returns>
        public async Task<bool> GenerateAndStoreImageGrids(int count)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                var imageHalfGenerator = scope.ServiceProvider.GetService<ImageHalfGenerator>();
                var logger = scope.ServiceProvider.GetService<ILogger>();

                try
                {
                    // path to the folder for today's image json files
                    string todaysPath = Path.Combine(FolderPath, DateTime.UtcNow.ToString(ImageGrid.DateFormatString));

                    // try create our folder for today if we haven't already
                    if (!IoHelper.TryCreateDirectoryIfNotExists(todaysPath))
                    {
                        throw new IOException("Failed to create daily folder");
                    }



                    // make count random image grids
                    for (int x = 0; x < count; x++)
                    {
                        var imageGrid = imageHalfGenerator.Generate();

                        // our indexes should be 1 to count
                        imageGrid.Index = x + 1;

                        context.ImageGrids.Add(imageGrid);

                        // serialize and write the image half 2d array to a file
                        string imageHalfJson = JsonConvert.SerializeObject(imageGrid.Half);
                        await File.WriteAllTextAsync(Path.Combine(todaysPath, $"{imageGrid.Index}.json"), imageHalfJson);
                    }

                    // push the changes to the db - should be count writes
                    return await context.SaveChangesAsync() == count;
                }

                catch (Exception ex)
                {
                    logger.Log("Generating / storing image grids", ex);
                    return false;
                }
            }
        }
    }
}
