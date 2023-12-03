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
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService; // Service pour gérer les utilisateurs

        public UsersController(UserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet("get-all-user")]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("get-user-by-id/{id}")]
        public ActionResult<User> GetUserById(Guid id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("create-new-user")]
        public ActionResult<User> CreateUser(User user)
        {
            var createdUser = _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("update-user/{id}")]
        public IActionResult UpdateUser(Guid id, User updatedUser)
        {
            var result = _userService.UpdateUser(id, updatedUser);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete("delete-user/{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            var result = _userService.DeleteUser(id);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpGet("user-comments/{id}")]
        public ActionResult<IEnumerable<Comments>> GetCommentsByUserId(Guid id)
        {
            var comments = _userService.GetCommentsByUserId(id);
            return Ok(comments);
        }

        [HttpGet("user-trips/{id}")]
        public ActionResult<IEnumerable<Voyage>> GetTripsByUserId(Guid id)
        {
            var trips = _userService.GetTripsByUserId(id);
            return Ok(trips);
        }
    }
}



/*
Récupérer tous les utilisateurs : Endpoint GET /api/users
Récupérer un utilisateur par ID : Endpoint GET /api/users/{id}
Créer un nouvel utilisateur : Endpoint POST /api/users
Mettre à jour un utilisateur existant : Endpoint PUT /api/users/{id}
Supprimer un utilisateur existant : Endpoint DELETE /api/users/{id}

Récupérer les commentaires d'un utilisateur :
Endpoint : GET /api/users/{id}/comments
Description : Cette route permet de récupérer les commentaires publiés par un utilisateur spécifique en utilisant son ID.

Récupérer les voyages d'un utilisateur :
Endpoint : GET /api/users/{id}/trips
Description : Cette route permet de récupérer les voyages effectués par un utilisateur spécifique, fournissant des détails sur les voyages planifiés ou passés associés à cet utilisateur.
*/