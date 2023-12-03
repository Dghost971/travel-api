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
    public class ActivityTypeController : ControllerBase
    {
        private readonly ActivityTypeService _activityTypeService;

        public ActivityTypeController(ActivityTypeService activityTypeService)
        {
            _activityTypeService = activityTypeService ?? throw new ArgumentNullException(nameof(activityTypeService));
        }

        // Obtient tous les types d'activités
        [HttpGet("get-all-activitytype")]
        public ActionResult<ServiceActionResult<IEnumerable<ActivityType>>> GetAllActivityTypes()
        {
            var activityTypes = _activityTypeService.GetAllActivityTypes();
            return ServiceActionResult<IEnumerable<ActivityType>>.FromSuccess(activityTypes);
        }

        // Obtient un type d'activité par ID
        [HttpGet("get-activitytype-by-id/{id}")]
        public ActionResult<ServiceActionResult<ActivityType>> GetActivityTypeById(Guid id)
        {
            var activityType = _activityTypeService.GetActivityTypeById(id);
            if (activityType == null)
            {
                return ServiceActionResult<ActivityType>.FromError("ActivityType not found", ServiceActionErrorReason.NotFound);
            }
            return ServiceActionResult<ActivityType>.FromSuccess(activityType);
        }

        // Crée un nouveau ActivityType
        [HttpPost("create-new-activitytype")]
        public ActionResult<ServiceActionResult<ActivityType>> CreateActivityType(ActivityType activityType)
        {
            var result = _activityTypeService.CreateActivityType(activityType);
            return result;
        }


        // Met à jour un type d'activité existant
        [HttpPut("update-activitytype/{id}")]
        public ActionResult<ServiceActionResult<ActivityType>> UpdateActivityType(Guid id, ActivityType updatedActivityType)
        {
            var result = _activityTypeService.UpdateActivityType(id, updatedActivityType);
            if (!result.IsSuccess)
            {
                return ServiceActionResult<ActivityType>.FromError("ActivityType not found", ServiceActionErrorReason.NotFound);
            }
            return ServiceActionResult<ActivityType>.FromSuccess(result.Result);
        }

        // Supprime un type d'activité existant
        [HttpDelete("delete-activitytype/{id}")]
        public ActionResult<ServiceActionResult<ActivityType>> DeleteActivityType(Guid id)
        {
            var result = _activityTypeService.DeleteActivityType(id);
            if (!result.IsSuccess)
            {
                return ServiceActionResult<ActivityType>.FromError("ActivityType not found", ServiceActionErrorReason.NotFound);
            }
            return ServiceActionResult<ActivityType>.FromSuccess(result.Result);
        }

        // Obtient toutes les activités associées à un type spécifique
        [HttpGet("related-activities/{id}")]
        public ActionResult<ServiceActionResult<IEnumerable<Activites>>> GetActivitiesForType(Guid id)
        {
            var activities = _activityTypeService.GetActivitiesForActivityType(id);
            return ServiceActionResult<IEnumerable<Activites>>.FromSuccess(activities.Result);
        }

        // Obtient les statistiques pour un type d'activité spécifique
        [HttpGet("stats/{id}")]
        public ActionResult<ServiceActionResult<ActivityTypeStats>> GetStatsForActivityType(Guid id)
        {
            var stats = _activityTypeService.GetStatsForActivityType(id);
            return ServiceActionResult<ActivityTypeStats>.FromSuccess(stats.Result);

        }
    }
}

/*
Récupérer tous les types d'activités : Endpoint GET /api/activitytypes
Récupérer un type d'activité par ID : Endpoint GET /api/activitytypes/{id}
Créer un nouveau type d'activité : Endpoint POST /api/activitytypes
Mettre à jour un type d'activité existant : Endpoint PUT /api/activitytypes/{id}
Supprimer un type d'activité existant : Endpoint DELETE /api/activitytypes/{id}

Récupérer les activités associées à un type spécifique :Endpoint: GET / api / activitytypes /{ id}/ activities
Description: Cette route permet de récupérer toutes les activités associées à un type spécifique d'activité.

Récupérer les statistiques d'utilisation pour un type d'activité donné :
Description: Cette route permet de récupérer des statistiques spécifiques d'utilisation pour un type d'activité particulier, telles que le nombre total d'activités associées à ce type, la date de création, etc.
*/
