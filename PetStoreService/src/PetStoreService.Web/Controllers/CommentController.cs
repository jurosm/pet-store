using Microsoft.AspNetCore.Mvc;
using PetStoreService.Application.Models.Request.Comment;
using PetStoreService.Application.Models.Response.Comment;
using PetStoreService.Application.Services.CommentsSystem;
using PetStoreService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetStoreService.Web.Controllers
{
    [Route("/comment")]
    public class CommentController(CommentService commentService) : BaseApiController
    {
        private readonly CommentService _commentService = commentService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentsUnit>>> GetComments([FromQuery] int toyId)
        {
            return Ok(await _commentService.GetCommentsFromToy(toyId));
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> CreateComment([FromBody] CommentData commentData)
        {
            var comment = await _commentService.CreateCommentAsync(commentData);
            return CreatedAtAction(nameof(CreateComment), comment);
        }
    }
}