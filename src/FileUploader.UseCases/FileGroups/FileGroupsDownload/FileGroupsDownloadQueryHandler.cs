using FileUploader.Infrastructure.Abstractions.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;

namespace FileUploader.UseCases.FileGroups.FileGroupsDownload;

internal class FileGroupsDownloadQueryHandler : IRequestHandler<FileGroupsDownloadQuery, FileGroupsDownloadQueryResult>
{
    private readonly IAppDbContext appDbContext;

    public FileGroupsDownloadQueryHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<FileGroupsDownloadQueryResult> Handle(FileGroupsDownloadQuery request, CancellationToken cancellationToken)
    {
        var fileGroup = await appDbContext.FileGroups
            .AsNoTracking()
            .Include(fg => fg.Files)
            .FirstOrDefaultAsync(fg => fg.Id == request.FileGroupId, cancellationToken)
            ?? throw new Exception("File group not found.");
        if (fileGroup.Files.Count == 1)
        {
            var file = fileGroup.Files.Single();
            var fileBytes = await File.ReadAllBytesAsync(file.Path, cancellationToken);
            using var fileStream = new MemoryStream(fileBytes);
            return new FileGroupsDownloadQueryResult() 
            { 
                ContentStream = fileStream,
                ContentName = file.Name,
                ContentType = "application/octet-stream"
            };
        }

        var zipName = $"FileGroup-{DateTime.UtcNow:yyyy_MM_dd-HH_mm_ss}.zip";
        using var ms = new MemoryStream();
        using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
        {
            foreach (var file in fileGroup.Files)
            {
                var entry = zip.CreateEntry(file.Name);
                var fileBytes = await File.ReadAllBytesAsync(file.Path, cancellationToken);
                using var fileStream = new MemoryStream(fileBytes);
                using var entryStream = entry.Open();
                await fileStream.CopyToAsync(entryStream, cancellationToken);
            }
        }

        return new FileGroupsDownloadQueryResult()
        {
            ContentStream = ms,
            ContentName = zipName,
            ContentType = "application/zip"
        };
    }
}
