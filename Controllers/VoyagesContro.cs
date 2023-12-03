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
    [ApiController]
    [Route("[controller]")]
    public class VoyagesController : ControllerBase
    {
        private readonly VoyageService _voyageService;

        public VoyagesController(VoyageService voyageService)
        {
            _voyageService = voyageService;
        }

        // Récupérer tous les voyages
        [HttpGet("get-all-voyages")]
        public IActionResult GetAllVoyages()
        {
            var voyages = _voyageService.GetAllVoyages();
            return Ok(voyages);
        }

        // Récupérer un voyage par ID
        [HttpGet("get-voyage-by-id/{id}")]
        public IActionResult GetVoyageById(Guid id)
        {
            var voyage = _voyageService.GetVoyageById(id);
            if (voyage == null)
            {
                return NotFound(); // Voyage non trouvé
            }
            return Ok(voyage);
        }

        // Ajouter un nouveau voyage
        [HttpPost("create-new-voyage")]
        public IActionResult AddVoyage([FromBody] Voyage voyage)
        {
            var newVoyage = _voyageService.AddVoyage(voyage);
            return Ok(newVoyage);
        }


        // Mettre à jour un voyage par ID
        [HttpPut("update-voyage/{id}")]
        public IActionResult UpdateVoyage(Guid id, [FromBody] Voyage voyage)
        {
            var isUpdated = _voyageService.UpdateVoyage(id, voyage);
            if (!isUpdated)
            {
                return NotFound(); // Voyage non trouvé
            }
            return NoContent(); // Succès de la mise à jour
        }

        // Supprimer un voyage par ID
        [HttpDelete("delete-voyage/{id}")]
        public IActionResult DeleteVoyage(Guid id)
        {
            var isDeleted = _voyageService.DeleteVoyage(id);
            if (!isDeleted)
            {
                return NotFound(); // Voyage non trouvé
            }
            return NoContent(); // Succès de la suppression
        }

        // Récupérer les voyages avec le nombre d'activités
        [HttpGet("voyages-activities-count")]
        public IActionResult GetVoyagesWithActivitiesCount()
        {
            var voyagesWithActivitiesCount = _voyageService.GetVoyagesWithActivitiesCount();
            return Ok(voyagesWithActivitiesCount);
        }
    }
}
