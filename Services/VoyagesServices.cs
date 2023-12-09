using System;
using System.Collections.Generic;
using System.Linq;
using TravelAPI.Models;
using TravelAPI.DBContexts;
using TravelAPI.Controllers;

namespace TravelAPI.Services
{
    public class VoyageService
    {
        private readonly TravelDbContext _dbContext;

        public VoyageService(TravelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Récupérer tous les voyages
        public IEnumerable<Voyage> GetAllVoyages()
        {
            return _dbContext.Voyages.ToList();
        }

        // Récupérer un voyage par ID
        public Voyage GetVoyageById(Guid id)
        {
            return _dbContext.Voyages.Find(id);
        }

        // Ajouter un nouveau voyage
        public Voyage AddVoyage(Voyage voyage)
        {
            var newVoyage = new Voyage
            {
                Id = Guid.NewGuid(),
                Name = voyage.Name,
                DestinationCountry = voyage.DestinationCountry,
                StartDate = voyage.StartDate,
                EndDate = voyage.EndDate,
                Activities = voyage.Activities
            };

            _dbContext.Voyages.Add(newVoyage);
            _dbContext.SaveChanges();

            return newVoyage;
        }

        // Mettre à jour un voyage par ID
        public bool UpdateVoyage(Guid id, Voyage voyage)
        {
            var existingVoyage = _dbContext.Voyages.Find(id);
            if (existingVoyage == null)
            {
                return false;
            }

            existingVoyage.Name = voyage.Name;
            existingVoyage.DestinationCountry = voyage.DestinationCountry;
            existingVoyage.StartDate = voyage.StartDate;
            existingVoyage.EndDate = voyage.EndDate;
            existingVoyage.Activities = voyage.Activities;

            _dbContext.SaveChanges();
            return true;
        }


        // Supprimer un voyage par ID
        public bool DeleteVoyage(Guid id)
        {
            var voyageToDelete = _dbContext.Voyages.Find(id);
            if (voyageToDelete == null)
            {
                return false;
            }

            _dbContext.Voyages.Remove(voyageToDelete);
            _dbContext.SaveChanges();
            return true;
        }

        // Récupérer les voyages avec le nombre d'activités
        public IEnumerable<object> GetVoyagesWithActivitiesCount()
        {
            return _dbContext.Voyages.Select(voyage => new
            {
                Voyage = voyage,
                ActivitiesCount = voyage.Activities.Count
            });
        }
    }
}
