using TravelAPI.Models;

namespace TravelAPI.Dto
{
    public class CommentsDTO
{
    public Guid Id { get; set; }
    public string Contents { get; set; }
    public DateTime PublicationDate { get; set; }
    public string UserName { get; set; } // Ajout du nom de l'auteur du commentaire
}

}