using PetStoreService.Domain.Entities;
using PetStoreService.Persistence;

namespace PetStoreService.Application.Services.CommentsSystem;

public class CommentRepository(PetStoreDBContext context) : Repository<Comment>(context)
{
    public IEnumerable<Comment> GetCommentsFromToy(int toyId)
    {
        return Table.Where(x => x.ToyId != null && x.ToyId == toyId);
    }
}