using FileUploader.UseCases.Users;

namespace FileUploader.Server.DependencyInjection;

public class AutoMapperModule
{
    public static void Register(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UsersMappingProfile).Assembly);
    }
}
