using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Models;
using SocialMedia.Application.UseCases.Posts.Commands;
using SocialMedia.Application.UseCases.Posts.Models;
using SocialMedia.Application.UseCases.Posts.Queries;

namespace SocialMedia.APi.Controllers;

public class PostController : ApiControllerBase
{
    [HttpPost]
    public async ValueTask<ActionResult<PostDto>> PostPostAsync(CreatePostCommand command)
    {
        PostDto dto = await Mediator.Send(command);

        return Ok(dto);
    }

    [HttpGet("{postId}")]
    public async ValueTask<ActionResult<PostDto>> GetPostAsync(Guid postId)
    {
        return await Mediator.Send(new GetPostQuery(postId));
    }

    [HttpGet]
    public async ValueTask<ActionResult<PaginatedList<PostDto>>> GetPostsWithPaginated([FromQuery] GetAllPostsQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPut]
    public async ValueTask<ActionResult<PostDto>> PutPostAsync(UpdatePostCommmand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete("{postId}")]
    public async ValueTask<ActionResult<PostDto>> DeletePostAsync(Guid postId)
    {
        return await Mediator.Send(new DeletePostCommand(postId));
    }
}
