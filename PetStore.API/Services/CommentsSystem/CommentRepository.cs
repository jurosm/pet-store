using PetStore.API.Db;
using PetStore.API.Services.CRUD;
using System.Collections.Generic;
using System.Linq;

namespace PetStore.API.Services.CommentsSystem
{
    public class CommentRepository(ContextWrapper<Comment> context) : Repository<Comment>(context)
    {
        public IEnumerable<Comment> GetCommentsFromToy(int toyId)
        {
            return Context.Table.Where(x => x.ToyId != null && x.ToyId == toyId);
        }
    }
}
