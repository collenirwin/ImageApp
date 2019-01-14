using ImageApp.Data;
using ImageApp.Models;
using ImageApp.Services.Logging;
using ImageApp.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ImageApp.Services
{
    public class CommentService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CommentService(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Adds the specified <see cref="Comment"/> to the database
        /// </summary>
        /// <param name="comment">Comment to add</param>
        /// <returns>true if successful</returns>
        public async Task<bool> AddCommentAsync(Comment comment)
        {
            // hook the comment up with a unique id
            comment.Id = Guid.NewGuid().ToString();

            // this comment was written right now
            comment.DateCreated = DateTime.UtcNow;

            try
            {
                _context.Comments.Add(comment);

                // save; should be 1 row changed
                return await _context.SaveChangesAsync() == 1;
            }
            catch (Exception ex)
            {
                _logger.Log($"Adding a comment: {comment?.Id ?? "null"}", ex);
                return false;
            }
        }

        /// <summary>
        /// Gets all of the comments on a specified <see cref="ImageHalf"/>
        /// as an <see cref="IQueryable"/> object
        /// </summary>
        /// <param name="imageHalfId">Id of an <see cref="ImageHalf"/> object</param>
        /// <returns>null if unsuccessful</returns>
        private IQueryable<Comment> GetCommentsQueryable(string imageHalfId)
        {
            try
            {
                return _context.Comments
                    .Where(comment => comment.ImageHalfId == imageHalfId);
            }
            catch (Exception ex)
            {
                _logger.Log($"Getting comments as IQueryable: {imageHalfId ?? "null"}", ex);
                return null;
            }
        }

        /// <summary>
        /// Gets all of the comments on a specified <see cref="ImageHalf"/>
        /// ordered by <see cref="Comment.DateCreated"/>
        /// </summary>
        /// <param name="imageHalfId">Id of an <see cref="ImageHalf"/> object</param>
        /// <param name="newestFirst">Sort descending?</param>
        /// <returns>null if unsuccessful</returns>
        public async Task<Comment[]> GetCommentsOrderedByDateCreatedAsync(string imageHalfId,
            bool newestFirst = true)
        {
            try
            {
                return await GetCommentsQueryable(imageHalfId)
                    .OrderBy(comment => comment.DateCreated, descending: newestFirst)
                    .ToArrayAsync();
            }
            catch (Exception ex)
            {
                _logger.Log($"Getting comments (newest first): {imageHalfId ?? "null"}", ex);
                return null;
            }
        }
    }
}
