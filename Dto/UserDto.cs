using TravelAPI.Models;

namespace TravelAPI.Dto
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public required string PhoneNumber { get; set; }
    }

    public class UserWithVoyagesDTO : UserDTO
    {
        public List<VoyageDTO> Voyages { get; set; }
    }

    public class UserWithCommentsDTO : UserDTO
    {
        public List<CommentsDTO> Comments { get; set; }
    }

}