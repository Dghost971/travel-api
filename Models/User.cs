namespace TravelAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } //Encrypted
        public DateTime RegistrationDate { get; set; }
        public string PhoneNumber { get; set; }
        public List<Voyage> Voyage { get; set; }
        public List<Comments> Comments { get; set; }

    }
}
