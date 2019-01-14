using System;
using System.ComponentModel.DataAnnotations;

namespace ImageApp.Models
{
    /// <summary>
    /// Represents a comment on an image
    /// </summary>
    public class Comment
    {
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Id of user who made this comment
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Id of the image this comment is on
        /// </summary>
        public string ImageHalfId { get; set; }

        /// <summary>
        /// Comment text itself
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Please make sure your comment is between {2} and {1} characters", MinimumLength = 1)]
        public string Text { get; set; }

        /// <summary>
        /// How many people thought this comment was good
        /// </summary>
        public int Upvotes { get; set; }

        /// <summary>
        /// How many people thought this comment was bad
        /// </summary>
        public int Downvotes { get; set; }

        /// <summary>
        /// Date and time this comment was written
        /// </summary>
        public DateTime DateCreated { get; set; }
    }
}
