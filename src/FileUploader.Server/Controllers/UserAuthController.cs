using FileUploader.UseCases.Users.LoginUser;
using FileUploader.UseCases.Users.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileUploader.Server.Controllers;

[ApiController]
[Route("api/auth")]
public class UserAuthController : Controller
{
    private readonly IMediator mediator;

    public UserAuthController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<LoginUserQueryResult> Login(LoginUserQuery query, CancellationToken cancellationToken = default)
         => await mediator.Send(query, cancellationToken);

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task Register(RegisterUserCommand addUserCommand)
        => await mediator.Send(addUserCommand);
}
