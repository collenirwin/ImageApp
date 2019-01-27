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
    /// <summary>
    /// Provides methods relating to Comments and their storage
    /// </summary>
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
        /// Gets all of the comments on a specified <see cref="ImageGrid"/>
        /// as an <see cref="IQueryable"/> object
        /// </summary>
        /// <param name="imageGridId">Id of an <see cref="ImageGrid"/> object</param>
        /// <returns>null if unsuccessful</returns>
        private IQueryable<Comment> GetCommentsQueryable(string imageGridId)
        {
            try
            {
                return _context.Comments
                    .Where(comment => comment.ImageGridId == imageGridId);
            }
            catch (Exception ex)
            {
                _logger.Log($"Getting comments as IQueryable: {imageGridId ?? "null"}", ex);
                return null;
            }
        }

        /// <summary>
        /// Gets all of the comments on a specified <see cref="ImageGrid"/>
        /// ordered by <see cref="Comment.DateCreated"/>
        /// </summary>
        /// <param name="imageGridId">Id of an <see cref="ImageGrid"/> object</param>
        /// <param name="newestFirst">Sort descending?</param>
        /// <returns>null if unsuccessful</returns>
        public async Task<Comment[]> GetCommentsOrderedByDateCreatedAsync(string imageGridId,
            bool newestFirst = true)
        {
            try
            {
                return await GetCommentsQueryable(imageGridId)
                    .OrderBy(comment => comment.DateCreated, descending: newestFirst)
                    .AsNoTracking()
                    .ToArrayAsync();
            }
            catch (Exception ex)
            {
                _logger.Log($"Getting comments (newest first): {imageGridId ?? "null"}", ex);
                return null;
            }
        }

        /// <summary>
        /// Gets the top rated comment on the specified image half
        /// </summary>
        /// <param name="imageGridId">Id of an <see cref="ImageGrid"/> object</param>
        /// <returns>null if unsuccessful</returns>
        public async Task<Comment> GetTopComment(string imageGridId)
        {
            try
            {
                return await _context.Comments
                    .Where(comment => comment.ImageGridId == imageGridId)
                    .OrderByDescending(comment => comment.Rating)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.Log($"Getting top comment for: {imageGridId ?? "null"}", ex);
                return null;
            }
        }
    }
}
