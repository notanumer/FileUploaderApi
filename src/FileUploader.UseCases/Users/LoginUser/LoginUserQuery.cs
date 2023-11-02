using MediatR;

namespace FileUploader.UseCases.Users.LoginUser;

public record LoginUserQuery : IRequest<LoginUserQueryResult>
{
    required public string Email { get; init; }

    required public string Password { get; init; }
}
