using Extensions.Hosting.AsyncInitialization;
using FileUploader.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace FileUploader.Server.Startup;

internal sealed class DatabaseInitializer : IAsyncInitializer
{
    private readonly AppDbContext appDbContext;

    /// <summary>
    /// Database initializer. Performs migration and data seed.
    /// </summary>
    /// <param name="appDbContext">Data context.</param>
    public DatabaseInitializer(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc />
    public async Task InitializeAsync()
    {
        await appDbContext.Database.MigrateAsync();
    }
}
