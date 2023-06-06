using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Models;
using SocialMedia.Application.UseCases.Users.Commands;
using SocialMedia.Application.UseCases.Users.Models;
using SocialMedia.Application.UseCases.Users.Queries;

namespace SocialMedia.APi.Controllers;


public class UserController : ApiControllerBase
{

    [HttpPost]
    public async ValueTask<ActionResult<UserDto>> PostUserAsync(CreateUserCommand command)
    {
        UserDto dto = await Mediator.Send(command);

        return Ok(dto);
    }


    [HttpGet("{userId}")]
    public async ValueTask<ActionResult<UserDto>> GetUserAsync(Guid userId)
    {
        return await Mediator.Send(new GetUserQuery(userId));
    }


    [HttpGet]
    [ResponseCache(Duration =20)]
    public async ValueTask<ActionResult<PaginatedList<UserDto>>> GetUsersWithPaginated([FromQuery] GetAllUsersQuery query)
    {
        return await Mediator.Send(query);
    }


    [HttpPut]
    public async ValueTask<ActionResult<UserDto>> PutUserAsync(UpdateUserCommand command)
    {
        return await Mediator.Send(command);
    }


    [HttpDelete("{userId}")]
    public async ValueTask<ActionResult<UserDto>> DeleteUserAsync(Guid userId)
    {
        return await Mediator.Send(new DeleteUserCommand(userId));
    }
}
