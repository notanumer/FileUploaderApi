using MediatR;

namespace FileUploader.UseCases.Users.RegisterUser;

public record RegisterUserCommand : IRequest
{
    required public string Email { get; init; }

    required public string Password { get; init; }

    required public string Name { get; init; }

    required public string LastName { get; init; }
}