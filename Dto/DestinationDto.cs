using TravelAPI.Models;

namespace TravelAPI.Dto
{
    public class DestinationDTO
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
    }

    public class DestinationWithActivitiesDTO : DestinationDTO
    {
        public List<ActivitesDTO> Activities { get; set; }
    }

    public class DestinationWithVoyagesDTO : DestinationDTO
    {
        public List<VoyageDTO> Voyages { get; set; }
    }

}