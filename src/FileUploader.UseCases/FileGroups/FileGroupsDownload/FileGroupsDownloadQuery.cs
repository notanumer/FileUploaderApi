using FileUploader.Infrastructure.Abstractions.Interfaces.Dto;
using MediatR;

namespace FileUploader.UseCases.FileGroups.FileGroupsDownload;

public record FileGroupsDownloadQuery(int FileGroupId) : IRequest<DownloadFileDto>;
