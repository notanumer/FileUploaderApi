using MediatR;

namespace FileUploader.UseCases.FileGroups.FileGroupsGenerateDownloadLink;

public record FileGroupsGenerateDownloadLinkQuery(int FileGroupId) : IRequest<string>;
