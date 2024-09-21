using Microsoft.AspNetCore.Mvc;
using PetStoreService.Application.Models.Request.Comment;
using PetStoreService.Application.Models.Response.Comment;
using PetStoreService.Application.Services.CommentsSystem;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetStoreService.Web.Controllers
{
    [Route("/comments")]
    public class CommentController(CommentService commentService) : BaseApiController
    {
        private readonly CommentService _commentService = commentService;

        [HttpGet("{toyId}")]
        public IEnumerable<CommentsUnit> GetComments(int toyId)
        {
            return _commentService.GetCommentsFromToy(toyId);
        }

        [HttpPost]
        public async Task CreateComment([FromBody] CommentData commentData)
        {
            await _commentService.CreateCommentAsync(commentData);
        }
    }
}