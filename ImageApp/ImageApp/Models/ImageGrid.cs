using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageApp.Models
{
    /// <summary>
    /// Contains some properties of an image grid (actual grid matrix stored as json in a file)
    /// </summary>
    public class ImageGrid
    {
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Keys: Color names (ex: CORAL),
        /// Values: hex color value (including #)
        /// </summary>
        public static readonly Dictionary<string, string> Colors =
            new Dictionary<string, string>()
        {
            { "CORAL",    "#FF847C" },
            { "GREEN",    "#BDECB6" },
            { "SEAGREEN", "#78B3A4" },
            { "ORANGE",   "#FFAA7D" },
            { "CYAN",     "#6CF3D5" },
            { "PURPLE",   "#B1B2CD" },
            { "BLUE",     "#A1BEE6" },
            { "YELLOW",   "#FFDE66" },
            { "RED",      "#F86254" }
        };

        /// <summary>
        /// Date format used in the naming of the daily folder
        /// </summary>
        public const string DateFormatString = "MM.dd.yyyy";

        /// <summary>
        /// The color of this image half (should be a key in <see cref="Colors"/>)
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Height and width of the image
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Represents half of the 'pixels' in the image grid
        /// </summary>
        [NotMapped]
        public bool[,] Half { get; set; }

        /// <summary>
        /// Optional daily index
        /// </summary>
        public int? Index { get; set; }

        /// <summary>
        /// Date and time this image half was created
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Url to this image's json file
        /// </summary>
        [NotMapped]
        public string Url
        {
            get => Index != null
                ? $"/images/daily/{DateCreated.ToString(DateFormatString)}/{Index}.json"
                : null;
        }

        /// <summary>
        /// The comments written about this image half
        /// </summary>
        public ICollection<Comment> Comments { get; set; }
    }
}
