using FileUploader.Domain;
using FileUploader.Infrastructure.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

namespace FileUploader.Infrastructure.DataAccess;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<User> Users { get; protected set; }

    public DbSet<FileGroup> FileGroups { get; protected set; }

    public DbSet<Domain.File> Files { get; protected set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        => await Database.BeginTransactionAsync(cancellationToken);

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ForceHavingAllStringsAsVarchars(modelBuilder);
        ApplyConfigurations(modelBuilder);
    }

    private static void ForceHavingAllStringsAsVarchars(ModelBuilder modelBuilder)
    {
        var stringColumns = modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(e => e.GetProperties())
            .Where(p => p.ClrType == typeof(string));
        foreach (IMutableProperty mutableProperty in stringColumns)
        {
            mutableProperty.SetIsUnicode(false);
        }
    }

    private void ApplyConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
