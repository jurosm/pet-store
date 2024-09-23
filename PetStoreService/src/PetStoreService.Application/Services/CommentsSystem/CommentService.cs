using AutoMapper;
using PetStoreService.Application.Models.Request.Comment;
using PetStoreService.Application.Models.Response.Comment;
using PetStoreService.Domain.Entities;

namespace PetStoreService.Application.Services.CommentsSystem;

public class CommentService(CommentRepository commentRepository, IMapper mapper)
{
    private readonly CommentRepository _commentRepository = commentRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<CommentsUnit>> GetCommentsFromToy(int toyId)
    {
        return (await _commentRepository.GetCommentsFromToy(toyId)).Select(_mapper.Map<CommentsUnit>);
    }

    public async Task<Comment> CreateCommentAsync(CommentData commentData)
    {
        var comment = await _commentRepository.CreateAsync(_mapper.Map<Comment>(commentData));
        return comment;
    }
}