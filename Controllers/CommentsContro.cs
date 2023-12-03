using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelAPI.Models;
using TravelAPI.DBContexts;
using TravelAPI.Services;
using Microsoft.EntityFrameworkCore;


namespace TravelAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentService _commentService;

        public CommentsController(CommentService commentService)
        {
            _commentService = commentService ?? throw new ArgumentNullException(nameof(commentService));
        }

        // GET: api/comments
        [HttpGet("get-all-comment")]
        public ActionResult<ServiceActionResult<IEnumerable<Comments>>> GetAllComments()
        {
            return Ok(_commentService.GetAllComments());
        }

        // GET: api/comments/{id}
        [HttpGet("get-comment-by-id/{id}")]
        public ActionResult<ServiceActionResult<Comments>> GetCommentById(Guid id)
        {
            var comments = _commentService.GetCommentById(id);
            if (comments == null)
            {
                return NotFound();
            }
            return Ok(comments);
        }

        // POST: api/comments
        [HttpPost("create-new-comment")]
        public ActionResult<ServiceActionResult<Comments>> CreateComment(Comments comments)
        {
            var createdComment = _commentService.CreateComment(comments);
            return CreatedAtAction(nameof(GetCommentById), new { id = createdComment.Result.Id }, createdComment.Result);
        }

        // PUT: api/comments/{id}
        [HttpPut("update-comment/{id}")]
        public IActionResult UpdateComment(Guid id, Comments updatedComment)
        {
            var result = _commentService.UpdateComment(id, updatedComment);
            if (!result.IsSuccess)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/comments/{id}
        [HttpDelete("delete-comment/{id}")]
        public IActionResult DeleteComment(Guid id)
        {
            var result = _commentService.DeleteComment(id);
            if (!result.IsSuccess)
            {
                return NotFound();
            }
            return NoContent();
        }

        // GET: api/comments/search?content={content}
        [HttpGet("search/content={content}")]
        public ActionResult<ServiceActionResult<IEnumerable<Comments>>> SearchCommentsByContent(string content)
        {
            var comments = _commentService.SearchCommentsByContent(content);
            return Ok(comments);
        }

        // GET: api/comments/by-date?date={YYYY-MM-DD}
        [HttpGet("get-comment-by-date/date={YYYY-MM-DD}")]
        public ActionResult<ServiceActionResult<IEnumerable<Comments>>> GetCommentsByDate(string date)
        {
            if (!DateTime.TryParse(date, out DateTime parsedDate))
            {
                return BadRequest("Invalid date format. Please use YYYY-MM-DD.");
            }

            var comments = _commentService.GetCommentsByDate(parsedDate);
            return Ok(comments);
        }
    }
}

/*
Récupérer tous les commentaires : Endpoint GET /api/comments
Récupérer un commentaire par ID : Endpoint GET /api/comments/{id}
Créer un nouveau commentaire : Endpoint POST /api/comments
Mettre à jour un commentaire existant : Endpoint PUT /api/comments/{id}
Supprimer un commentaire existant : Endpoint DELETE /api/comments/{id}

Rechercher des commentaires par contenu :Endpoint : GET /api/comments/search?content={content}
Description : Cette route permet de rechercher des commentaires en fonction de leur contenu, permettant aux utilisateurs de trouver des commentaires spécifiques en utilisant des mots-clés.

Récupérer les commentaires publiés à une date spécifique :Endpoint : GET /api/comments/by-date?date={YYYY-MM-DD}
Description : Cette route permet de récupérer tous les commentaires publiés à une date spécifique en utilisant le format YYYY-MM-DD pour la date. Cela permet aux utilisateurs de sélectionner une date précise pour obtenir les commentaires publiés ce jour-là.
*/
