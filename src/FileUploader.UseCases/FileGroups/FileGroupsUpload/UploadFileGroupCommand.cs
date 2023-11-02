using MediatR;
using Microsoft.AspNetCore.Http;

namespace FileUploader.UseCases.FileGroups.FileGroupsUpload;

public record UploadFileGroupCommand(ICollection<IFormFile> Files) : IRequest;
