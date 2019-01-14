using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImageApp.Models
{
    /// <summary>
    /// Represents half of an image grid
    /// </summary>
    public class ImageHalf
    {
        [Key]
        [Required]
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

        [Required]
        [JsonProperty("color")]
        public string Color { get; set; }

        [Required]
        [JsonProperty("size")]
        public int Size { get; set; }

        [Required]
        [JsonProperty("pixels")]
        public bool[,] Pixels { get; set; }

        [Required]
        [JsonProperty("dateCreated")]
        public DateTime DateCreated { get; set; }
    }
}
