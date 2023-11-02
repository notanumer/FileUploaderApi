using AutoMapper;
using FileUploader.Domain;
using FileUploader.Infrastructure.Abstractions.Interfaces;
using MediatR;
using System.Security.Cryptography;
using System.Text;

namespace FileUploader.UseCases.Users.RegisterUser;

internal class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
{
    private readonly IAppDbContext dbContext;
    private readonly IMapper mapper;

    public RegisterUserCommandHandler(IAppDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(request);
        HashPassword(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        user.CreatedAt = DateTimeOffset.UtcNow;
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private void HashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }
}
