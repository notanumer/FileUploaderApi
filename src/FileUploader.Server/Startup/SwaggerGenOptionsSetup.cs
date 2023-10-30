using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics;
using System.Reflection;

namespace FileUploader.Server.Startup;

internal class SwaggerGenOptionsSetup
{
    public void Setup(SwaggerGenOptions options)
    {
        var fileVersionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);

        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = fileVersionInfo.ProductVersion,
            Title = "Swipepick API",
            Description = "API documentation for the project."
        });

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Description = "Bearer Authorization with JWT",
            Type = SecuritySchemeType.Http
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                new List<string>()
            }
        });
    }
}
