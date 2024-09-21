using PetStoreService.Domain.Entities;

namespace PetStoreService.Application.Services.CommentsSystem;

public class CommentRepository(ContextWrapper<Comment> context) : Repository<Comment>(context)
{
    public IEnumerable<Comment> GetCommentsFromToy(int toyId)
    {
        return Context.Table.Where(x => x.ToyId != null && x.ToyId == toyId);
    }
}
