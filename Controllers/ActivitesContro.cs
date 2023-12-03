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
    public class ActivitiesController : ControllerBase
    {
        private readonly ActivityService _activityService;

        public ActivitiesController(ActivityService activityService)
        {
            _activityService = activityService;
        }

        // Récupère toutes les activités
        [HttpGet("get-all-activities")]
        public ActionResult<IEnumerable<Activites>> GetActivities()
        {
            var activities = _activityService.GetAllActivities();
            return Ok(activities);
        }

        // Récupère une activité par ID
        [HttpGet("get-activities-by-id/{id}")]
        public ActionResult<Activites> GetActivityById(Guid id)
        {
            var Activites = _activityService.GetActivityById(id);
            if (Activites == null)
            {
                return NotFound();
            }
            return Ok(Activites);
        }

        // Ajoute une nouvelle activité
        [HttpPost("add-new-activities")]
        public ActionResult<Activites> AddActivity(Activites activites)
        {
            var addedActivity = _activityService.AddActivity(activites);
            return Ok(addedActivity);
        }

        // Met à jour une activité par ID
        [HttpPut("update-activities/{id}")]
        public IActionResult UpdateActivity(Guid id, Activites updatedActivity)
        {
            var success = _activityService.UpdateActivity(id, updatedActivity);
            if (!success)
            {
                return NotFound("Activites not found.");
            }
            return NoContent();
        }

        // Supprime une activité par ID
        [HttpDelete("delete-activities/{id}")]
        public IActionResult DeleteActivity(Guid id)
        {
            var success = _activityService.DeleteActivity(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Récupère les activités par type
        [HttpGet("activities-by-type")]
        public ActionResult<IEnumerable<Activites>> GetActivitiesByType([FromQuery] string type)
        {
            var activities = _activityService.GetActivitiesByType(type);
            return Ok(activities);
        }

        // Récupère les activités à venir
        [HttpGet("upcoming-activities")]
        public ActionResult<IEnumerable<Activites>> GetUpcomingActivities()
        {
            var activities = _activityService.GetUpcomingActivities();
            return Ok(activities);
        }
    }
}