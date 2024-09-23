using Microsoft.EntityFrameworkCore;
using PetStoreService.Domain.Entities;
using PetStoreService.Persistence;

namespace PetStoreService.Application.Services.CommentsSystem;

public class CommentRepository(PetStoreDBContext context) : Repository<Comment>(context)
{
    public Task<List<Comment>> GetCommentsFromToy(int toyId)
    {
        return Table.Where(x => x.ToyId != null && x.ToyId == toyId).ToListAsync();
    }
}