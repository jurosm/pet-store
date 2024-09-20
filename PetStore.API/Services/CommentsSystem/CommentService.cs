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
        private readonly CommentRepository _commentRepository = commentRepository;
        private readonly IMapper _mapper = mapper;

        public IEnumerable<CommentsUnit> GetCommentsFromToy(int toyId)
        {
            return _commentRepository.GetCommentsFromToy(toyId).Select(x => _mapper.Map<CommentsUnit>(x));
        }

        public async Task CreateCommentAsync(CommentData commentData)
        {
            await _commentRepository.CreateAsync(_mapper.Map<Comment>(commentData));
        }
    }
}