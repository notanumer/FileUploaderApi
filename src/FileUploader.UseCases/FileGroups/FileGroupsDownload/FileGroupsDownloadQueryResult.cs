namespace FileUploader.UseCases.FileGroups.FileGroupsDownload;

public record FileGroupsDownloadQueryResult
{
    required public MemoryStream ContentStream { get; init; }
    
    required public string ContentType { get; init; }

    required public string ContentName { get; init; }
}
