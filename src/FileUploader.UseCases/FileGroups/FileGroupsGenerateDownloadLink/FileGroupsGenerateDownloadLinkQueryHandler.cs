using FileUploader.Infrastructure.Abstractions.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace FileUploader.UseCases.FileGroups.FileGroupsGenerateDownloadLink;

internal partial class FileGroupsGenerateDownloadLinkQueryHandler : IRequestHandler<FileGroupsGenerateDownloadLinkQuery, string>
{
    private readonly IAppDbContext appDbContext;
    private readonly IConfiguration configuration;

    public FileGroupsGenerateDownloadLinkQueryHandler(IAppDbContext appDbContext, IConfiguration configuration)
    {
        this.appDbContext = appDbContext;
        this.configuration = configuration;
    }

    public async Task<string> Handle(FileGroupsGenerateDownloadLinkQuery request, CancellationToken cancellationToken)
    {
        var fileGroup = await appDbContext.FileGroups
            .FirstOrDefaultAsync(fg => fg.Id == request.FileGroupId, cancellationToken)
            ?? throw new Exception("File group not found.");
        var token = BaseStringRegex().Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "");
        fileGroup.Token = token;
        await appDbContext.SaveChangesAsync(cancellationToken);
        var host = configuration.GetRequiredSection("ServerUrl");
        return new Uri($"{host.Value}/api/file-group/{token}/download").ToString();

    }

    [GeneratedRegex("[/+=]")]
    private static partial Regex BaseStringRegex();
}
