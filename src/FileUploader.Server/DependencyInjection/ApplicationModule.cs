using FileUploader.Infrastructure.Abstractions.Interfaces;
using FileUploader.Infrastructure.Implimentations.Services;
using FileUploader.Server.Web;

namespace FileUploader.Server.DependencyInjection;

internal static class ApplicationModule
{
    public static void Register(IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ILoggedUserAccessor, LoggedUserAccessor>();
        services.AddSingleton<IUploadFilesService, FileUploadService>();
        services.AddScoped<IDownloadFilesService, DownloadFilesService>();
    }
}
