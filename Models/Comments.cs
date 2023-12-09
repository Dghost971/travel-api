namespace TravelAPI.Models
{
    public class Comments
    {
        public Guid Id { get; set; }
        public required string Contents { get; set; } //Comments content 
        public required User UserName { get; set; } // Reference to the User class
        public DateTime PublicationDate { get; set; }

    }
}