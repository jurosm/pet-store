using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetStore.API.Models.Request.Comment;
using PetStore.API.Models.Response.Comment;
using PetStore.API.Services.CommentsSystem;

namespace PetStore.API.Controllers
{
    [Route("/comments")]
    public class CommentController(CommentService commentService) : BaseApiController
    {
        private readonly CommentService CommentService = commentService;

        [HttpGet("{toyId}")]
        public IEnumerable<CommentsUnit> GetComments(int toyId)
        {
            return CommentService.GetCommentsFromToy(toyId);
        }

        [HttpPost]
        public async Task CreateComment([FromBody] CommentData commentData)
        {
            await CommentService.CreateCommentAsync(commentData);
        }
    }
}
