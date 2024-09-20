using AutoMapper;
using PetStore.API.Db;
using PetStore.API.Models.Request.Comment;
using PetStore.API.Models.Response.Comment;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Services.CommentsSystem
{
    public class CommentService(CommentRepository commentRepository, IMapper mapper)
    {
        private readonly CommentRepository CommentRepository = commentRepository;
        private readonly IMapper Mapper = mapper;

        public IEnumerable<CommentsUnit> GetCommentsFromToy(int toyId)
        {
            return CommentRepository.GetCommentsFromToy(toyId).Select(x => Mapper.Map<CommentsUnit>(x));
        }

        public async Task CreateCommentAsync(CommentData commentData)
        {
            await CommentRepository.CreateAsync(Mapper.Map<Comment>(commentData));
        }
    }
}