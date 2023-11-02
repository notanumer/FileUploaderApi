using FileUploader.UseCases.FileGroups.FileGroupsGetUploaded.FileGroupsGetUploadedDto;
using MediatR;

namespace FileUploader.UseCases.FileGroups.FileGroupsGetUploaded;

public record FileGroupsGetUploadedQuery : IRequest<ICollection<FileGroupDto>>;
