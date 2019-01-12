using ImageApp.Data;
using ImageApp.Models;
using ImageApp.Services.Logging;
using System;
using System.Collections.Generic;
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

        public async Task<bool> AddComment(Comment comment)
        {
            // hook the comment up with a unique id
            comment.Id = Guid.NewGuid().ToString();

            try
            {
                _context.Comments.Add(comment);

                // save; should be 1 row changed
                return await _context.SaveChangesAsync() == 1;
            }
            catch (Exception ex)
            {
                _logger.Log("Adding a comment: " + comment.Id, ex);
                return false;
            }
        }
    }
}
