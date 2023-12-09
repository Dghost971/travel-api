using TravelAPI.Models;

namespace TravelAPI.Dto
{
    public class ActivitesDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public ActivityTypeDTO ActivityType { get; set; }
        public List<Guid> VoyageIds { get; set; }
    }

}