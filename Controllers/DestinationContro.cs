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

