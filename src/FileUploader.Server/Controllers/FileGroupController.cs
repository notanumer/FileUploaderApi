using FileUploader.UseCases.FileGroups.FileGroupGetFileProgress;
using FileUploader.UseCases.FileGroups.FileGroupsDownload;
using FileUploader.UseCases.FileGroups.FileGroupsDownloadByToken;
using FileUploader.UseCases.FileGroups.FileGroupsGenerateDownloadLink;
using FileUploader.UseCases.FileGroups.FileGroupsGetAllProgress;
using FileUploader.UseCases.FileGroups.FileGroupsGetUploaded;
using FileUploader.UseCases.FileGroups.FileGroupsGetUploaded.FileGroupsGetUploadedDto;
using FileUploader.UseCases.FileGroups.FileGroupsUpload;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileUploader.Server.Controllers;

/// <summary>
/// Controller for files.
/// </summary>
[ApiController]
[Route("api/file-group")]
public class FileGroupController : Controller
{
    private readonly IMediator mediator;

    public FileGroupController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// Upload file(s).
    /// Accessible only for authenticated users.
    /// </summary>
    /// <param name="files">Selected files.</param>
    [Authorize]
    [HttpPost("upload")]
    public async Task UploadFiles(ICollection<IFormFile> files, CancellationToken cancellationToken = default)
    {
        await mediator.Send(new UploadFileGroupCommand(files), cancellationToken);
    }

    /// <summary>
    /// Gets progress of a file upload by its name.
    /// If no name is specified, returns the progress of the entire group of files.
    /// Accessible only for authenticated users.
    /// </summary>
    /// <param name="fileName">File name. (Optional)</param>
    /// <returns>Upload progress in %.</returns>
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

    /// <summary>
    /// Gets all uploaded user's files.
    /// Accessible only for authenticated users.
    /// </summary>
    /// <returns>Collection of files.</returns>
    [Authorize]
    [HttpGet("uploaded")]
    public async Task<ICollection<FileGroupDto>> GetUploadedFileGroups(CancellationToken cancellationToken = default)
    {
        return await mediator.Send(new FileGroupsGetUploadedQuery(), cancellationToken);
    }

    /// <summary>
    /// Download own file group by group Id.
    /// Accessible only for authenticated users.
    /// </summary>
    /// <param name="id">Group id.</param>
    /// <returns>File <see cref="FileContentResult"/>.</returns>
    [Authorize]
    [HttpGet("download/{id:int}")]
    public async Task<IActionResult> DownloadOwnFileGroup([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var content = await mediator.Send(new FileGroupsDownloadQuery(id), cancellationToken);
        return File(content.ContentStream.ToArray(), content.ContentType, content.ContentName);
    }

    /// <summary>
    /// Generate file(s) one-time link for downloading.
    /// Accessible only for authenticated users.
    /// </summary>
    /// <param name="id">File group id.</param>
    /// <returns>One-time link.</returns>
    [Authorize]
    [HttpGet("generate/{id:int}/link")]
    public async Task<string> GetFileDownloadLink([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var fileGroupLink = await mediator.Send(new FileGroupsGenerateDownloadLinkQuery(id), cancellationToken);
        return fileGroupLink;
    }

    /// <summary>
    /// Download file(s) by one-time link.
    /// Accessible for all users.
    /// </summary>
    /// <param name="token">File group token.</param>
    /// <returns>File <see cref="FileContentResult"/>.</returns>
    [AllowAnonymous]
    [HttpGet("{token}/download")]
    public async Task<IActionResult> DownloadFileGroupByLink([FromRoute] string token, CancellationToken cancellationToken = default)
    {
        var content = await mediator.Send(new FileGroupsDownloadByTokenQuery(token), cancellationToken);
        return File(content.ContentStream.ToArray(), content.ContentType, content.ContentName);
    }
}
