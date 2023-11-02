using FileUploader.Domain;
using FileUploader.Infrastructure.Abstractions.Interfaces;
using MediatR;

namespace FileUploader.UseCases.FileGroups.FileGroupsUpload;

internal class UploadFileGroupCommandHandler : IRequestHandler<UploadFileGroupCommand>
{
    private readonly IAppDbContext appDbContext;
    private readonly ILoggedUserAccessor loggedUserAccessor;
    private readonly IUploadFilesService uploadFilesService;

    public UploadFileGroupCommandHandler(
        IUploadFilesService uploadFilesService,
        ILoggedUserAccessor loggedUserAccessor,
        IAppDbContext appDbContext)
    {
        this.uploadFilesService = uploadFilesService;
        this.loggedUserAccessor = loggedUserAccessor;
        this.appDbContext = appDbContext;
    }

    /// <inheritdoc/>
    public async Task Handle(UploadFileGroupCommand request, CancellationToken cancellationToken)
    {
        var userId = loggedUserAccessor.GetCurrentUserId();
        var fileGroup = new FileGroup
        {
            CreatedByUserId = userId,
            CreatedAt = DateTimeOffset.UtcNow,
        };

        uploadFilesService.FillUploadProgressWithEmptyValues(request.Files.Select(f => f.FileName).ToList(), userId);

        foreach (var file in request.Files)
        {
            var filePathLocal = await uploadFilesService.UploadUserFileAsync(file, userId, cancellationToken);
            var userFile = new Domain.File()
            {
                Name = file.FileName,
                Type = file.ContentType,
                Path = filePathLocal
            };
            fileGroup.Files.Add(userFile);
        }

        appDbContext.FileGroups.Add(fileGroup);
        await appDbContext.SaveChangesAsync(cancellationToken);
        uploadFilesService.ClearFileUploadProgress();
    }
}
