using FileUploader.Domain;
using Microsoft.EntityFrameworkCore;

namespace FileUploader.Infrastructure.Abstractions.Interfaces;

public interface IDbContextWithSets
{
    DbSet<User> Users { get; }

    DbSet<FileGroup> FileGroups { get; }

    DbSet<Domain.File> Files { get; }

    /// <summary>
    /// Get the set of entities by type.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    /// <returns>Set of entities.</returns>
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    /// <summary>
    /// Save pending changes.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>Number of affected rows.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
