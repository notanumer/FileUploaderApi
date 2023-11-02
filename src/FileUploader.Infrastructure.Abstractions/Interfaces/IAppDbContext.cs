using Microsoft.EntityFrameworkCore.Storage;

namespace FileUploader.Infrastructure.Abstractions.Interfaces;

public interface IAppDbContext : IDbContextWithSets, IDisposable
{
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
}
