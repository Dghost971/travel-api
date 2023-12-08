namespace TravelAPI.Models
{
    public class Comments
    {
        public Guid Id { get; set; }
        public string Contents { get; set; } //Comments content 
        public User AuthorName { get; set; } // Reference to the User class
        public DateTime PublicationDate { get; set; }

    }
}