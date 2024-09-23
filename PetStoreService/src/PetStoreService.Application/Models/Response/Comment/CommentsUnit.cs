
namespace PetStoreService.Application.Models.Response.Comment;

public class CommentsUnit
{
    public required string Text { get; set; }
    public DateTime DatePosted { get; set; }
    public required string Author { get; set; }
}