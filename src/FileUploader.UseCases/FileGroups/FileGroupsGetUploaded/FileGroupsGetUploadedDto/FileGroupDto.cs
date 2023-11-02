namespace FileUploader.UseCases.FileGroups.FileGroupsGetUploaded.FileGroupsGetUploadedDto;

public record FileGroupDto
{
    public int Id { get; init; }

    public DateTimeOffset CreatedAt { get; init; }

    public ICollection<FileDto> Files { get; init; } = new List<FileDto>();
}
