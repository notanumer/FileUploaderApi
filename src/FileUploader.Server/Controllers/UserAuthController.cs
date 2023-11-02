using FileUploader.UseCases.Users.LoginUser;
using FileUploader.UseCases.Users.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileUploader.Server.Controllers;

/// <summary>
/// Controller for user authentication/registration.
/// </summary>
[ApiController]
[Route("api/auth")]
public class UserAuthController : Controller
{
    private readonly IMediator mediator;

    public UserAuthController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// Use this endpoint to login.
    /// </summary>
    /// <param name="query">Contains email and password.</param>
    /// <returns>JWT.</returns>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<LoginUserQueryResult> Login(LoginUserQuery query, CancellationToken cancellationToken = default)
         => await mediator.Send(query, cancellationToken);

    /// <summary>
    /// Use this endpoint to register.
    /// </summary>
    /// <param name="addUserCommand">Contains info about new user.</param>
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task Register(RegisterUserCommand addUserCommand)
        => await mediator.Send(addUserCommand);
}
