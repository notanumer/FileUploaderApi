using FileUploader.UseCases.Users.RegisterUser;

namespace FileUploader.Server.DependencyInjection;

public class MediatRModule
{
    public static void Register(IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly));
    }
}
