namespace TravelAPI.Models
{
    public class Activites
    {
        // Properties with auto-implemented getters and setters
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public Guid ActivityTypeId { get; set; }
        public ActivityType? ActivityType { get; set; }
        public List<Voyage>? Voyages { get; set; } //List of voyages associated with this Activites.
    }
}
