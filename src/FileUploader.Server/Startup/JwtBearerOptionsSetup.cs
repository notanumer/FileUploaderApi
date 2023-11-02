using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FileUploader.Server.Startup;

internal class JwtBearerOptionsSetup
{
    private readonly string secretKey;
    private readonly string issuer;

    public JwtBearerOptionsSetup(string issuer, string secretKey)
    {
        this.issuer = issuer;
        this.secretKey = secretKey;
    }

    public void Setup(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    }
}
