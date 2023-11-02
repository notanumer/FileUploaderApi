using AutoMapper;
using AutoMapper.QueryableExtensions;
using FileUploader.Infrastructure.Abstractions.Interfaces;
using FileUploader.UseCases.FileGroups.FileGroupsGetUploaded.FileGroupsGetUploadedDto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FileUploader.UseCases.FileGroups.FileGroupsGetUploaded;

internal class FileGroupsGetUploadedQueryHandler : IRequestHandler<FileGroupsGetUploadedQuery, ICollection<FileGroupDto>>
{
    private readonly ILoggedUserAccessor loggedUserAccessor;
    private readonly IMapper mapper;
    private readonly IAppDbContext appDbContext;

    public FileGroupsGetUploadedQueryHandler(
        IAppDbContext appDbContext,
        IMapper mapper, 
        ILoggedUserAccessor loggedUserAccessor)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
        this.loggedUserAccessor = loggedUserAccessor;
    }

    public async Task<ICollection<FileGroupDto>> Handle(FileGroupsGetUploadedQuery request, CancellationToken cancellationToken)
    {
        var userId = loggedUserAccessor.GetCurrentUserId();
        return await appDbContext.FileGroups
            .Where(fg => fg.CreatedByUserId == userId)
            .ProjectTo<FileGroupDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
