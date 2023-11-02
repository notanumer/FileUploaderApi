namespace FileUploader.UseCases.FileGroups.FileGroupsGetUploaded.FileGroupsGetUploadedDto;

public record FileDto
{
    required public string Name { get; init; }

    required public string Type { get; init; }
}
