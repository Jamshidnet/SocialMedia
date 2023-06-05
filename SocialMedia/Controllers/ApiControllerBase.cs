using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SocialMedia.APi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiControllerBase : ControllerBase
{
    private IMediator? _mediator;
    public IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();


}
