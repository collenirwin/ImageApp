namespace ImageApp.Models
{
    /// <summary>
    /// Represents a comment on an image
    /// </summary>
    public class Comment
    {
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
        public string Text { get; set; }

        /// <summary>
        /// How many people thought this comment was good
        /// </summary>
        public int Upvotes { get; set; }

        /// <summary>
        /// How many people thought this comment was bad
        /// </summary>
        public int Downvotes { get; set; }
    }
}
