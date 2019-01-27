using System.ComponentModel.DataAnnotations;

namespace ImageApp.Models
{
    /// <summary>
    /// Contains an image half and a comment - for use in the Daily view
    /// </summary>
    public class DailyViewModel
    {
        [Required]
        public ImageGrid ImageHalf { get; set; }

        [Required]
        public Comment Comment { get; set; }
    }
}
