namespace FileUploader.Infrastructure.Abstractions.Interfaces.Dto;

public record DownloadFileDto
{
    required public MemoryStream ContentStream { get; init; }

    required public string ContentType { get; init; }

    required public string ContentName { get; init; }
}
