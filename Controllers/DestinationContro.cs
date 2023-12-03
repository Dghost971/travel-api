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
    public class DestinationsController : ControllerBase
    {
        private readonly DestinationService _destinationService;

        public DestinationsController(DestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        // GET: api/destinations
        [HttpGet("get-all-destination")]
        public ActionResult<IEnumerable<Destination>> GetAllDestinations()
        {
            // Récupère toutes les destinations
            var destinations = _destinationService.GetAllDestinations();
            return Ok(destinations);
        }

        // GET: api/destinations/{id}
        [HttpGet("get-destination-by-id/{id}")]
        public ActionResult<Destination> GetDestinationById(Guid id)
        {
            // Récupère une destination par ID
            var destination = _destinationService.GetDestinationById(id);
            if (destination == null)
            {
                return NotFound();
            }
            return Ok(destination);
        }

        // POST: api/destinations
        [HttpPost("create-new-destination")]
        public ActionResult<Destination> CreateDestination(Destination destination)
        {
            // Crée une nouvelle destination
            var createdDestination = _destinationService.CreateDestination(destination);
            return CreatedAtAction(nameof(GetDestinationById), new { id = createdDestination.Id }, createdDestination);
        }

        // PUT: api/destinations/{id}
        [HttpPut("update-destination/{id}")]
        public IActionResult UpdateDestination(Guid id, Destination updatedDestination)
        {
            // Met à jour une destination existante
            var result = _destinationService.UpdateDestination(id, updatedDestination);
            if (!result.IsSuccess)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/destinations/{id}
        [HttpDelete("delete-destination/{id}")]
        public IActionResult DeleteDestination(Guid id)
        {
            // Supprime une destination existante
            var result = _destinationService.DeleteDestination(id);
            if (!result.IsSuccess)
            {
                return NotFound();
            }
            return NoContent();
        }


        // GET: api/destinations/{id}/popular-activities
        [HttpGet("popular-activities/{id}")]
        public ActionResult<IEnumerable<Activites>> GetPopularActivitiesForDestination(Guid id)
        {
            // Récupère les activités populaires pour une destination donnée
            var popularActivities = _destinationService.GetPopularActivitiesForDestination(id);
            return Ok(popularActivities);
        }
    }
}

/*
Récupérer tous les destinations : Endpoint GET /api/destinations
Récupérer une destination par ID : Endpoint GET /api/destinations/{id}
Créer une nouvelle destination : Endpoint POST /api/destinations
Mettre à jour une destination existante : Endpoint PUT /api/destinations/{id}
Supprimer une destination existante : Endpoint DELETE /api/destinations/{id}

Récupérer les voyages disponibles pour une destination donnée :

Endpoint : GET /api/destinations/{id}/available-voyages
Description : Cette route permet de récupérer la liste des voyages disponibles associés à une destination spécifique, en utilisant l'ID de la destination.
Récupérer les informations sur les activités populaires pour une destination :

Endpoint : GET /api/destinations/{id}/popular-activities
Description : Cette route permet de récupérer des détails sur les activités populaires associées à une destination spécifique, en utilisant l'ID de la destination.

*/