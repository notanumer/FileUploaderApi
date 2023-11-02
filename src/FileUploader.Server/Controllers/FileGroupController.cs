using FileUploader.UseCases.FileGroups.FileGroupGetFileProgress;
using FileUploader.UseCases.FileGroups.FileGroupsGetAllProgress;
using FileUploader.UseCases.FileGroups.FileGroupsGetUploaded;
using FileUploader.UseCases.FileGroups.FileGroupsGetUploaded.FileGroupsGetUploadedDto;
using FileUploader.UseCases.FileGroups.FileGroupsUpload;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileUploader.Server.Controllers;

[ApiController]
[Route("api/file-group")]
public class FileGroupController : Controller
{
    private readonly IMediator mediator;

    public FileGroupController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [Authorize]
    [HttpPost("upload")]
    public async Task UploadFiles(ICollection<IFormFile> files, CancellationToken cancellationToken = default)
    {
        await mediator.Send(new UploadFileGroupCommand(files), cancellationToken);
    }

    [Authorize]
    [HttpGet("progress")]
    public async Task<double> GetProgress(string? fileName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            return await mediator.Send(new FileGroupsGetAllProgressQuery(), cancellationToken);
        }

        return await mediator.Send(new FileGroupGetFileProgressQuery(fileName), cancellationToken);
    }

    [Authorize]
    [HttpGet("uploaded")]
    public async Task<ICollection<FileGroupDto>> GetUploadedFileGroups(CancellationToken cancellationToken = default)
    {
        return await mediator.Send(new FileGroupsGetUploadedQuery(), cancellationToken);
    }
}
