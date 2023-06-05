using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Models;
using SocialMedia.Application.UseCases.Comments.Commands;
using SocialMedia.Application.UseCases.Comments.Models;
using SocialMedia.Application.UseCases.Comments.Queries;
using System.Data;

namespace SocialMedia.APi.Controllers;

public class CommentController : ApiControllerBase
{
    [HttpPost] 
    public async ValueTask<ActionResult<CommentDto>> PostCommentAsync(CreateCommentCommand command)
    {
        CommentDto dto = await Mediator.Send(command);

        return Ok(dto);
    }

    [HttpGet("{commentId}")]
    public async ValueTask<ActionResult<CommentDto>> GetCommentAsync(Guid commentId)
    {
        return await Mediator.Send(new GetCommentQuery(commentId));
    }

    [HttpGet]
    public async ValueTask<ActionResult<PaginatedList<CommentDto>>> GetCommentsWithPaginated([FromQuery] GetAllCommentsQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPut]
    public async ValueTask<ActionResult<CommentDto>> PutCommentAsync(UpdateCommentCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete("{commentId}")]
    public async ValueTask<ActionResult<CommentDto>> DeleteCommentAsync(Guid commentId)
    {
        return await Mediator.Send(new DeleteCommentCommand(commentId));
    }
}
