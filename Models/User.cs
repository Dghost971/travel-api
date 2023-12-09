namespace TravelAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; } //Encrypted
        public DateTime RegistrationDate { get; set; }
        public required string PhoneNumber { get; set; }
        public List<Voyage>? Voyage { get; set; }
        public List<Comments>? Comments { get; set; }

    }
}
