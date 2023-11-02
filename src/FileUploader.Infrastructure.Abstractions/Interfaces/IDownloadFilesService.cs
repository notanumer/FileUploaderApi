using FileUploader.Infrastructure.Abstractions.Interfaces.Dto;
using File = FileUploader.Domain.File;

namespace FileUploader.Infrastructure.Abstractions.Interfaces;

public interface IDownloadFilesService
{
    Task<DownloadFileDto> GetFilesGroupToDownloadAsync(ICollection<File> files, CancellationToken cancellationToken);
}
