using ImageApp.Models;
using ImageApp.Utils;
using ImageGeneration;
using System;

namespace ImageApp.Services.ImageGeneration
{
    /// <summary>
    /// Used for generating <see cref="ImageHalf"/> objects with random properties within defined ranges
    /// </summary>
    public class ImageHalfGenerator
    {
        /// <summary>
        /// Sizes allowed for generated image half
        /// </summary>
        private readonly int[] _sizes = { 4, 6, 8, 10 };

        /// <summary>
        /// Fill chances allowed for generated image half
        /// </summary>
        private readonly double[] _fillChances = { 0.25, 0.5, 0.75 };

        /// <summary>
        /// Random object used in generating image halves
        /// </summary>
        private readonly Random _random = new Random();

        /// <summary>
        /// Generate an <see cref="ImageHalf"/> object with random properties within defined ranges
        /// </summary>
        public ImageHalf Generate()
        {
            // use the ImageGeneration library to spin up an image half
            var imageHalf = ImageGenerator
                .GenerateImageHalf(_random.Choose(_sizes), _random.Choose(_fillChances));

            // make our own with its properties, a random color, and the current date
            return new ImageHalf
            {
                Color = _random.Choose(ImageHalf.Colors.Keys),
                Size = imageHalf.Size,
                Pixels = imageHalf.Pixels,
                DateCreated = DateTime.UtcNow
            };
        }
    }
}
