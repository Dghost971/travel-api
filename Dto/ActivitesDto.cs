using TravelAPI.Models;

namespace TravelAPI.Dto
{
    public class ActivitesDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime Date { get; set; }
        public required string StartTime { get; set; }
        public required string EndTime { get; set; }
        public required ActivityTypeDTO ActivityType { get; set; }
        public List<Guid>? VoyageIds { get; set; }
    }

}