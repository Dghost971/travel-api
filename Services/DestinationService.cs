using System;
using System.Collections.Generic;
using System.Linq;
using TravelAPI.Models;
using TravelAPI.DBContexts;
using TravelAPI.Services;
using TravelAPI.Controllers;

namespace TravelAPI.Services
{
    public class DestinationService
    {
        private readonly TravelDbContext _TravelDbContext;

        public DestinationService(TravelDbContext dbContext)
        {
            _TravelDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IEnumerable<Destination> GetAllDestinations()
        {
            return _TravelDbContext.Destination.ToList();
        }

        public Destination GetDestinationById(Guid id)
        {
            return _TravelDbContext.Destination.FirstOrDefault(d => d.Id == id);
        }

        public Destination CreateDestination(Destination destination)
        {
            destination.Id = Guid.NewGuid();
            _TravelDbContext.Destination.Add(destination);
            _TravelDbContext.SaveChanges();
            return destination;
        }

        public ServiceActionResult UpdateDestination(Guid id, Destination updatedDestination)
        {
            var destination = _TravelDbContext.Destination.FirstOrDefault(d => d.Id == id);
            if (destination == null)
            {
                return ServiceActionResult.FromError("Destination not found", ServiceActionErrorReason.NotFound);
            }

            destination.Country = updatedDestination.Country;
            _TravelDbContext.SaveChanges();
            return ServiceActionResult.FromSuccess();
        }

        public ServiceActionResult DeleteDestination(Guid id)
        {
            var destination = _TravelDbContext.Destination.FirstOrDefault(d => d.Id == id);
            if (destination == null)
            {
                return ServiceActionResult.FromError("Destination not found", ServiceActionErrorReason.NotFound);
            }

            _TravelDbContext.Destination.Remove(destination);
            _TravelDbContext.SaveChanges();
            return ServiceActionResult.FromSuccess();
        }


        public IEnumerable<Activites> GetPopularActivitiesForDestination(Guid id)
        {
            var destination = _TravelDbContext.Destination.FirstOrDefault(d => d.Id == id);
            if (destination == null)
            {
                return new List<Activites>();
            }

            return destination.Activities ?? new List<Activites>();
        }
    }
}