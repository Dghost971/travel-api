
namespace TravelAPI.Models
{
    public class Destination
    {
        public Guid Id { get; set; }
        public string Country { get; set; } // Country where the destination is located
        public List<Activites> Activities { get; set; } // List of activities available or typical in this destination
        public List<Voyage> Voyage { get; set; } // List of voyages associated with this destination
    }

}