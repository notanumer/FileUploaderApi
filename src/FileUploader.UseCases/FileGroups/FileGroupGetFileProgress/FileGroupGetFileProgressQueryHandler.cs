using FileUploader.Infrastructure.Abstractions.Exceptions;
using FileUploader.Infrastructure.Abstractions.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploader.UseCases.FileGroups.FileGroupGetFileProgress;

internal class FileGroupGetFileProgressQueryHandler : IRequestHandler<FileGroupGetFileProgressQuery, double>
{
    private readonly ILoggedUserAccessor loggedUserAccessor;
    private readonly IUploadFilesService uploadFilesService;

    public FileGroupGetFileProgressQueryHandler(
        IUploadFilesService uploadFilesService,
        ILoggedUserAccessor loggedUserAccessor)
    {
        this.uploadFilesService = uploadFilesService;
        this.loggedUserAccessor = loggedUserAccessor;
    }

    public Task<double> Handle(FileGroupGetFileProgressQuery request, CancellationToken cancellationToken)
    {
        var userId = loggedUserAccessor.GetCurrentUserId();
        if (uploadFilesService.TryGetFileUploadProgress(userId, request.FileName, out var progress))
        {
            return Task.FromResult(progress);
        }

        throw new FileNotFoundException($"The specified file {request.FileName} was not found.");
    }
}
