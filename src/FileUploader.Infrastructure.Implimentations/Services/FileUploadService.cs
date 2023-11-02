using FileUploader.Infrastructure.Abstractions.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FileUploader.Infrastructure.Implimentations.Services;

public class FileUploadService : IUploadFilesService
{
    private readonly Dictionary<string, double> fileUploadProgress = new();

    public string StoragePath => $"Storage{Path.DirectorySeparatorChar}UsersFiles{Path.DirectorySeparatorChar}";

    public void ClearFileUploadProgress()
    {
        fileUploadProgress.Clear();
    }

    public void FillUploadProgressWithEmptyValues(ICollection<string> fileNames, int userId)
    {
        foreach (var fileName in fileNames)
        {
            fileUploadProgress.Add($"{userId}@{fileName}", 0);
        }
    }

    public double GetFileGroupUploadProgress(int userId)
    {
        return fileUploadProgress
            .Where(x => x.Key.Split("@").First() == userId.ToString())
            .Select(x => x.Value)
            .Average();
    }

    public bool TryGetFileUploadProgress(int userId, string fileName, out double progress)
    {
        if (!fileUploadProgress.ContainsKey($"{userId}@{fileName}"))
        {
            progress = 0;
            return false;
        }

        progress = fileUploadProgress[$"{userId}@{fileName}"];
        return true;
    }

    public async Task<string> UploadUserFileAsync(IFormFile file, int userId, CancellationToken cancellationToken)
    {
        var dir = $"Storage{Path.DirectorySeparatorChar}UsersFiles{Path.DirectorySeparatorChar}{userId}";
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        var fileName = file.FileName;
        var filePath = Path.Combine(dir, fileName);

        using var fileStream = File.Open(filePath, FileMode.Create);
        using var fileContentStream = file.OpenReadStream();
        var buffer = new byte[16 * 1024];
        int bytesRead;
        long totalFileBytesRead = 0;
        long fileContentLength = file.Length;
        while ((bytesRead = fileContentStream.Read(buffer, 0, buffer.Length)) > 0)
        {
            fileStream.Write(buffer, 0, bytesRead);
            totalFileBytesRead += bytesRead;
            fileUploadProgress[$"{userId}@{fileName}"] = (double)totalFileBytesRead / fileContentLength * 100;
            await Task.Delay(50, cancellationToken); // I used it for demonstartion.
        }

        return filePath;
    }
}
