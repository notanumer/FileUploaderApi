using FileUploader.Infrastructure.Abstractions.Interfaces;
using MediatR;

namespace FileUploader.UseCases.FileGroups.FileGroupsGetAllProgress;

internal class FileGroupsGetAllProgressQueryHandler : IRequestHandler<FileGroupsGetAllProgressQuery, double>
{
    private readonly ILoggedUserAccessor loggedUserAccessor;
    private readonly IUploadFilesService uploadFilesService;

    public FileGroupsGetAllProgressQueryHandler(
        IUploadFilesService uploadFilesService,
        ILoggedUserAccessor loggedUserAccessor)
    {
        this.uploadFilesService = uploadFilesService;
        this.loggedUserAccessor = loggedUserAccessor;
    }

    /// <inheritdoc/>
    public Task<double> Handle(FileGroupsGetAllProgressQuery request, CancellationToken cancellationToken)
    {
        var progress = uploadFilesService.GetFileGroupUploadProgress(loggedUserAccessor.GetCurrentUserId());
        return Task.FromResult(progress);
    }
}
