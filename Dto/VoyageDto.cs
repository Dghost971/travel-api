using TravelAPI.Models;

namespace TravelAPI.Dto
{
    public class VoyageDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string DestinationCountry { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Activites>? Activities { get; set; }
        public Guid UserId { get; set; }
    }
}

