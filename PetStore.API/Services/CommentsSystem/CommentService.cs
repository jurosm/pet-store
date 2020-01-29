using AutoMapper;
using PetStore.API.Db;
using PetStore.API.Models.Request.Comment;
using PetStore.API.Models.Response.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.API.Services.CommentsSystem
{
    public class CommentService
    {
        CommentRepository CommentRepository;
        IMapper Mapper;
        public CommentService(CommentRepository commentRepository, IMapper mapper)
        {
            this.CommentRepository = commentRepository;
            this.Mapper = mapper;
        }

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
