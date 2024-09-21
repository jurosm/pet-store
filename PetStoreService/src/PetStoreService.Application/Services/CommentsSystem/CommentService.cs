using AutoMapper;
using PetStoreService.Application.Models.Request.Comment;
using PetStoreService.Application.Models.Response.Comment;
using PetStoreService.Domain.Entities;

namespace PetStoreService.Application.Services.CommentsSystem;

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