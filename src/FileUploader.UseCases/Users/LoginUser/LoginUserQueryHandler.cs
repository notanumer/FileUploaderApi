using FileUploader.Domain;
using FileUploader.Infrastructure.Abstractions.Interfaces;
using FileUploader.UseCases.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FileUploader.UseCases.Users.LoginUser;

internal class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, LoginUserQueryResult>
{
    private readonly IAppDbContext dbContext;
    private readonly IConfiguration configuration;

    public LoginUserQueryHandler(IAppDbContext dbContext, IConfiguration configuration)
    {
        this.dbContext = dbContext;
        this.configuration = configuration;
    }

    public async Task<LoginUserQueryResult> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user != null)
        {
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new UserNotFoundException("User not found.");
            }

            return new LoginUserQueryResult()
            {
                UserEmail = user.Email,
                Token = GenerateJwt(user)
            };
        }

        throw new UserNotFoundException("User not found.");
    }

    private List<Claim> GetClaims(User user)
    {
        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim("Date", DateTimeOffset.UtcNow.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

        return claims;
    }

    private string GenerateJwt(User user)
    {
        var jwtSecret = configuration["JwtAuth:Key"] ?? throw new ArgumentNullException("JwtAuth:Key");
        var jwtIssuer = configuration["JwtAuth:Issuer"] ?? throw new ArgumentNullException("JwtAuth:Issuer");
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = GetClaims(user);
        var token = new JwtSecurityToken(jwtIssuer,
          jwtIssuer,
          claims,
          expires: DateTime.Now.AddMinutes(120),
          signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computeHash.SequenceEqual(passwordHash);
    }
}
