using FileUploader.Infrastructure.Abstractions.Interfaces;
using FileUploader.Infrastructure.Abstractions.Interfaces.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FileUploader.UseCases.FileGroups.FileGroupsDownload;

internal class FileGroupsDownloadQueryHandler : IRequestHandler<FileGroupsDownloadQuery, DownloadFileDto>
{
    private readonly IAppDbContext appDbContext;
    private readonly IDownloadFilesService downloadFilesService;

    public FileGroupsDownloadQueryHandler(IAppDbContext appDbContext, IDownloadFilesService downloadFilesService)
    {
        this.appDbContext = appDbContext;
        this.downloadFilesService = downloadFilesService;
    }

    public async Task<DownloadFileDto> Handle(FileGroupsDownloadQuery request, CancellationToken cancellationToken)
    {
        var fileGroup = await appDbContext.FileGroups
            .AsNoTracking()
            .Include(fg => fg.Files)
            .FirstOrDefaultAsync(fg => fg.Id == request.FileGroupId, cancellationToken)
            ?? throw new Exception("File group not found.");
        return await downloadFilesService.GetFilesGroupToDownloadAsync(fileGroup.Files, cancellationToken);
    }
}
