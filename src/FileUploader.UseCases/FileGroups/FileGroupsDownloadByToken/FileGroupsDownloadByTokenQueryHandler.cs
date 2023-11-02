using FileUploader.Infrastructure.Abstractions.Interfaces;
using FileUploader.Infrastructure.Abstractions.Interfaces.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FileUploader.UseCases.FileGroups.FileGroupsDownloadByToken;

internal class FileGroupsDownloadByTokenQueryHandler : IRequestHandler<FileGroupsDownloadByTokenQuery, DownloadFileDto>
{
    private readonly IAppDbContext appDbContext;
    private readonly IDownloadFilesService downloadFilesService;

    public FileGroupsDownloadByTokenQueryHandler(IDownloadFilesService downloadFilesService, IAppDbContext appDbContext)
    {
        this.downloadFilesService = downloadFilesService;
        this.appDbContext = appDbContext;
    }

    public async Task<DownloadFileDto> Handle(FileGroupsDownloadByTokenQuery request, CancellationToken cancellationToken)
    {
        var filesGroup = await appDbContext.FileGroups
            .Where(fg => !fg.DownloadedByTokenAt.HasValue)
            .SingleOrDefaultAsync(fg => fg.Token == request.Token, cancellationToken)
            ?? throw new Exception("File group not found.");
        filesGroup.DownloadedByTokenAt = DateTimeOffset.UtcNow;
        await appDbContext.SaveChangesAsync(cancellationToken);
        return await downloadFilesService.GetFilesGroupToDownloadAsync(filesGroup.Files, cancellationToken);
    }
}
