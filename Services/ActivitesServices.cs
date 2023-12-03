using System;
using System.Collections.Generic;
using System.Linq;
using TravelAPI.Models;
using TravelAPI.DBContexts;
using TravelAPI.Services;
using TravelAPI.Controllers;

namespace TravelAPI.Services
{
    public class ActivityService
    {
        private readonly TravelDbContext _context; // Utilisation du DbContext

        public ActivityService(TravelDbContext context)
        {
            _context = context;
        }

        // Récupère toutes les activités
        public IEnumerable<Activites> GetAllActivities()
        {
            return _context.Activities.ToList();
        }

        // Récupère une activité par ID
        public Activites GetActivityById(Guid id)
        {
            return _context.Activities.FirstOrDefault(a => a.Id == id);
        }

        // Ajoute une nouvelle activité
        public Activites AddActivity(Activites activites)
        {
            activites.Id = Guid.NewGuid();
            _context.Activities.Add(activites);
            _context.SaveChanges();
            return activites;
        }

        // Met à jour une activité par ID
        public bool UpdateActivity(Guid id, Activites updatedActivity)
        {
            var existingActivity = _context.Activities.FirstOrDefault(a => a.Id == id);
            if (existingActivity == null)
            {
                return false;
            }

            // Mettre à jour les détails de l'activité
            existingActivity.Name = updatedActivity.Name;
            existingActivity.Description = updatedActivity.Description;
            existingActivity.Date = updatedActivity.Date;
            existingActivity.ActivityType = updatedActivity.ActivityType;

            _context.SaveChanges();
            return true;
        }

        // Supprime une activité par ID
        public bool DeleteActivity(Guid id)
        {
            var activityToRemove = _context.Activities.FirstOrDefault(a => a.Id == id);
            if (activityToRemove == null)
            {
                return false;
            }

            _context.Activities.Remove(activityToRemove);
            _context.SaveChanges();
            return true;
        }

        // Récupère les activités par type
        public IEnumerable<Activites> GetActivitiesByType(string activityType)
        {
            return _context.Activities
                .Where(a => a.ActivityType.Name.Equals(activityType, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Récupère les activités à venir
        public IEnumerable<Activites> GetUpcomingActivities()
        {
            return _context.Activities.Where(a => a.Date > DateTime.Now).ToList();
        }
    }
}