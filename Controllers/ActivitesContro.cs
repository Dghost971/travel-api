using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelAPI.Models;
using TravelAPI.DBContexts;
using TravelAPI.Services;
using TravelAPI.Dto;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace TravelAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly ActivityService _activityService;
        private readonly IMapper _mapper; // Inject AutoMapper

        public ActivitiesController(ActivityService activityService, IMapper mapper)
        {
            _activityService = activityService;
            _mapper = mapper;
        }

        // Récupère toutes les activités
        [HttpGet("get-all-activities")]
        public ActionResult<IEnumerable<ActivitesDTO>> GetActivities()
        {
            var activities = _activityService.GetAllActivities();
            var ActivitesDTOs = _mapper.Map<IEnumerable<Activites>, IEnumerable<ActivitesDTO>>(activities);
            return Ok(ActivitesDTOs);
        }

        [HttpGet("get-activities-by-id/{id}")]
        public ActionResult<ActivitesDTO> GetActivityById(Guid id)
        {
            var activity = _activityService.GetActivityById(id);
            if (activity == null)
            {
                return NotFound();
            }
            var ActivitesDTO = _mapper.Map<Activites, ActivitesDTO>(activity);
            return Ok(ActivitesDTO);
        }

        [HttpPost("add-new-activities")]
        public ActionResult<ActivitesDTO> AddActivity(ActivitesDTO activitesDTO)
        {
            var activity = _mapper.Map<ActivitesDTO, Activites>(activitesDTO);
            var addedActivity = _activityService.AddActivity(activity);
            var addedActivitesDTO = _mapper.Map<Activites, ActivitesDTO>(addedActivity);
            return Ok(addedActivitesDTO);
        }

        [HttpPut("update-activities/{id}")]
        public IActionResult UpdateActivity(Guid id, ActivitesDTO updatedActivitesDTO)
        {
            var updatedActivity = _mapper.Map<ActivitesDTO, Activites>(updatedActivitesDTO);
            var success = _activityService.UpdateActivity(id, updatedActivity);
            if (!success)
            {
                return NotFound("Activity not found.");
            }
            return NoContent();
        }

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

        [HttpGet("activities-by-type")]
        public ActionResult<IEnumerable<ActivitesDTO>> GetActivitiesByType([FromQuery] string type)
        {
            var activities = _activityService.GetActivitiesByType(type);
            var activitesDTOs = _mapper.Map<IEnumerable<Activites>, IEnumerable<ActivitesDTO>>(activities);
            return Ok(activitesDTOs);
        }

        [HttpGet("upcoming-activities")]
        public ActionResult<IEnumerable<ActivitesDTO>> GetUpcomingActivities()
        {
            var activities = _activityService.GetUpcomingActivities();
            var activitesDTOs = _mapper.Map<IEnumerable<Activites>, IEnumerable<ActivitesDTO>>(activities);
            return Ok(activitesDTOs);
        }
    }
}