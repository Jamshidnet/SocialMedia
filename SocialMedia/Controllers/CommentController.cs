using LazyCache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SocialMedia.Application.Models;
using SocialMedia.Application.UseCases.Comments.Commands;
using SocialMedia.Application.UseCases.Comments.Models;
using SocialMedia.Application.UseCases.Comments.Queries;

namespace SocialMedia.APi.Controllers;

public class CommentController : ApiControllerBase
{

    IAppCache _lazyCache;
    IMemoryCache _memoryCache;

    public CommentController(IAppCache lazyCache, IMemoryCache memoryCache)
    {
        _lazyCache = lazyCache;
        _memoryCache = memoryCache;
    }

    [HttpPost]
    public async ValueTask<ActionResult<CommentDto>> PostCommentAsync(CreateCommentCommand command)
    {
        CommentDto dto = await Mediator.Send(command);

        return Ok(dto);
    }

    [HttpGet("{commentId}")]
    public async ValueTask<ActionResult<CommentDto?>> GetCommentAsync(Guid commentId)
    {
        return await _memoryCache.GetOrCreateAsync(My_Key,
            async x =>
            {
                x.SetAbsoluteExpiration(TimeSpan.FromSeconds(20));
                return await Mediator.Send(new GetCommentQuery(commentId));
            });
    }


    [HttpGet]
    public async ValueTask<ActionResult<PaginatedList<CommentDto>>> GetCommentsWithPaginated([FromQuery] GetAllCommentsQuery query)
    {
        return await _lazyCache.GetOrAddAsync(My_Key,
            async x =>
        {
            x.SetAbsoluteExpiration(TimeSpan.FromSeconds(20));
            return Ok(await Mediator.Send(query));
        });
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
