using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace SocialMedia.APi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiControllerBase : ControllerBase
{
    private IMediator? _mediator;
    public IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

    protected readonly string My_Key = "ThiS iS kEy";
    protected readonly string My_Second_Key = "ThiS iS SeConD kEy";

}
