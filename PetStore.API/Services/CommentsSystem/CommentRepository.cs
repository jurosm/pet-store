using PetStore.API.Db;
using PetStore.API.Services.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Services.CommentsSystem
{
    public class CommentRepository : Repository<Comment>
    {
        public CommentRepository(ContextWrapper<Comment> context) : base(context) { }

        public IEnumerable<Comment> GetCommentsFromToy(int toyId)
        {
            return Context.Table.Where(x => x.ToyId != null && x.ToyId == toyId);
        }
    }
}
