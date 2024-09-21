using System.ComponentModel.DataAnnotations;

namespace PetStoreService.Application.Models.Request.Comment;

public class CommentData
{
    [Required]
    public string Text { get; set; }

    [Required]
    public int ToyId { get; set; }

    [Required]
    [MaxLength(20)]
    public string Author { get; set; }
}