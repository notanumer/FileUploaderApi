using FileUploader.Infrastructure.Abstractions.Interfaces;
using FileUploader.Infrastructure.Abstractions.Interfaces.Dto;
using System.IO.Compression;
using File = System.IO.File;

namespace FileUploader.Infrastructure.Implimentations.Services;

public class DownloadFilesService : IDownloadFilesService
{
    public async Task<DownloadFileDto> GetFilesGroupToDownloadAsync(ICollection<Domain.File> files, CancellationToken cancellationToken)
    {
        if (files.Count == 1)
        {
            var file = files.Single();
            var fileBytes = await File.ReadAllBytesAsync(file.Path, cancellationToken);
            using var fileStream = new MemoryStream(fileBytes);
            return new DownloadFileDto()
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
            foreach (var file in files)
            {
                var entry = zip.CreateEntry(file.Name);
                var fileBytes = await File.ReadAllBytesAsync(file.Path, cancellationToken);
                using var fileStream = new MemoryStream(fileBytes);
                using var entryStream = entry.Open();
                await fileStream.CopyToAsync(entryStream, cancellationToken);
            }
        }

        return new DownloadFileDto()
        {
            ContentStream = ms,
            ContentName = zipName,
            ContentType = "application/zip"
        };
    }
}
