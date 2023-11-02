using FileUploader.Infrastructure.Abstractions.Interfaces.Dto;
using MediatR;

namespace FileUploader.UseCases.FileGroups.FileGroupsDownloadByToken;

public record FileGroupsDownloadByTokenQuery(string Token) : IRequest<DownloadFileDto>;
