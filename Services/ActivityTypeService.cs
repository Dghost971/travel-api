using System;
using System.Collections.Generic;
using System.Linq;
using TravelAPI.Models;
using TravelAPI.DBContexts;
using TravelAPI.Controllers;

namespace TravelAPI.Services
{
    public class ActivityTypeService
    {
        private readonly TravelDbContext _dbContext;

        public ActivityTypeService(TravelDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IEnumerable<ActivityType> GetAllActivityTypes()
        {
            return _dbContext.ActivityType.ToList();
        }

        public ActivityType GetActivityTypeById(Guid id)
        {
            return _dbContext.ActivityType.FirstOrDefault(a => a.Id == id);
        }

        public ServiceActionResult<IEnumerable<Activites>> GetActivitiesForActivityType(Guid id)
        {
            try
            {
                var activitiesForType = _dbContext.Activities.Where(a => a.ActivityType.Id == id).ToList();

                if (!activitiesForType.Any())
                {
                    return ServiceActionResult<IEnumerable<Activites>>.FromError("No activities found for this ActivityType", ServiceActionErrorReason.NotFound);
                }

                return ServiceActionResult<IEnumerable<Activites>>.FromSuccess(activitiesForType);
            }
            catch (Exception ex)
            {
                return ServiceActionResult<IEnumerable<Activites>>.FromError(ex.Message, ServiceActionErrorReason.BusinessRule);
            }
        }


        public ServiceActionResult<ActivityTypeStats> GetStatsForActivityType(Guid id)
        {
            try
            {
                var activityType = _dbContext.ActivityType.FirstOrDefault(a => a.Id == id);
                if (activityType == null)
                {
                    return ServiceActionResult<ActivityTypeStats>.FromError("ActivityType not found", ServiceActionErrorReason.NotFound);
                }

                int numberOfActivities = _dbContext.Activities.Count(a => a.ActivityType.Id == id);

                var activityStats = new ActivityTypeStats
                {
                    TotalActivities = numberOfActivities
                };

                return ServiceActionResult<ActivityTypeStats>.FromSuccess(activityStats);
            }
            catch (Exception ex)
            {
                return ServiceActionResult<ActivityTypeStats>.FromError(ex.Message, ServiceActionErrorReason.BusinessRule);
            }
        }


        public ServiceActionResult<ActivityType> CreateActivityType(ActivityType activityType)
        {
            try
            {
                activityType.Id = Guid.NewGuid();
                _dbContext.ActivityType.Add(activityType);
                _dbContext.SaveChanges();
                return ServiceActionResult<ActivityType>.FromSuccess(activityType);
            }
            catch (Exception ex)
            {
                return ServiceActionResult<ActivityType>.FromError(ex.Message, ServiceActionErrorReason.BusinessRule);
            }
        }


        public ServiceActionResult<ActivityType> UpdateActivityType(Guid id, ActivityType updatedActivity)
        {
            try
            {
                var activitytype = _dbContext.ActivityType.FirstOrDefault(a => a.Id == id);
                if (activitytype == null)
                {
                    return ServiceActionResult<ActivityType>.FromError("ActivityType not found", ServiceActionErrorReason.NotFound);
                }

                activitytype.Name = updatedActivity.Name;
                // Assurez-vous de mettre à jour d'autres champs si nécessaire

                _dbContext.SaveChanges();
                return ServiceActionResult<ActivityType>.FromSuccess(activitytype);
            }
            catch (Exception ex)
            {
                return ServiceActionResult<ActivityType>.FromError(ex.Message, ServiceActionErrorReason.BusinessRule);
            }
        }


        public ServiceActionResult<ActivityType> DeleteActivityType(Guid id)
        {
            try
            {
                var activityType = _dbContext.ActivityType.FirstOrDefault(a => a.Id == id);
                if (activityType == null)
                {
                    return ServiceActionResult<ActivityType>.FromError("ActivityType not found", ServiceActionErrorReason.NotFound);
                }

                _dbContext.ActivityType.Remove(activityType);
                _dbContext.SaveChanges();
                return ServiceActionResult<ActivityType>.FromSuccess(activityType);
            }
            catch (Exception ex)
            {
                return ServiceActionResult<ActivityType>.FromError(ex.Message, ServiceActionErrorReason.BusinessRule);
            }
        }

    }
}