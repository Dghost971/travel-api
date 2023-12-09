namespace TravelAPI.Models
{
    public class Voyage
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string DestinationCountry { get; set; } // Destination of the voyage (reference to the Destination class).
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Activites>? Activities { get; set; } // List of activities associated with this voyage.
        public Guid UserId { get; set; } // Reference to the User class, representing the user associated with this voyage.
    }
}
