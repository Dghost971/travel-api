using TravelAPI.Models;

namespace TravelAPI.Dto
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string PhoneNumber { get; set; }
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