using Microsoft.AspNetCore.Http;

namespace FileUploader.Infrastructure.Abstractions.Interfaces;

public interface IUploadFilesService
{
    bool TryGetFileUploadProgress(int userId, string fileName, out double progress);

    double GetFileGroupUploadProgress(int userId);

    Task<string> UploadUserFileAsync(IFormFile file, int userId, CancellationToken cancellationToken);

    public string StoragePath { get; }

    public void ClearFileUploadProgress();

    public void FillUploadProgressWithEmptyValues(ICollection<string> fileNames, int userId);
}
