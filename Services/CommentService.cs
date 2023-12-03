using System;
using System.Collections.Generic;
using System.Linq;
using TravelAPI.Models;
using TravelAPI.DBContexts;
using TravelAPI.Services;
using TravelAPI.Controllers;

namespace TravelAPI.Services
{
    public class CommentService
    {
        private readonly TravelDbContext _dbContext; // Votre DbContext pour les commentaires

        public CommentService(TravelDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IEnumerable<Comments> GetAllComments()
        {
            return _dbContext.Comments.ToList();
        }

        public Comments GetCommentById(Guid id)
        {
            return _dbContext.Comments.FirstOrDefault(c => c.Id == id);
        }

        public ServiceActionResult<Comments> CreateComment(Comments comments)
        {
            comments.Id = Guid.NewGuid();
            _dbContext.Comments.Add(comments);
            _dbContext.SaveChanges();
            return ServiceActionResult<Comments>.FromSuccess(comments);
        }

        public ServiceActionResult<Comments> UpdateComment(Guid id, Comments updatedComment)
        {
            var existingComment = _dbContext.Comments.FirstOrDefault(c => c.Id == id);
            if (existingComment == null)
            {
                return ServiceActionResult<Comments>.FromError("Comments not found", ServiceActionErrorReason.NotFound);
            }

            existingComment.Contents = updatedComment.Contents;
            existingComment.AuthorName = updatedComment.AuthorName;
            // Mettre à jour d'autres propriétés si nécessaire

            _dbContext.SaveChanges();
            return ServiceActionResult<Comments>.FromSuccess(existingComment);
        }

        public ServiceActionResult<Comments> DeleteComment(Guid id)
        {
            var existingComment = _dbContext.Comments.FirstOrDefault(c => c.Id == id);
            if (existingComment == null)
            {
                return ServiceActionResult<Comments>.FromError("Comments not found", ServiceActionErrorReason.NotFound);
            }

            _dbContext.Comments.Remove(existingComment);
            _dbContext.SaveChanges();
            return ServiceActionResult<Comments>.FromSuccess(existingComment);
        }

        public IEnumerable<Comments> SearchCommentsByContent(string content)
        {
            return _dbContext.Comments.Where(c => c.Contents.Contains(content)).ToList();
        }

        public IEnumerable<Comments> GetCommentsByDate(DateTime date)
        {
            return _dbContext.Comments.Where(c => c.PublicationDate.Date == date.Date).ToList();
        }
    }
}
