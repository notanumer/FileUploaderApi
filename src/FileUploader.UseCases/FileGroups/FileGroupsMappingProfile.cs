using AutoMapper;
using FileUploader.Domain;
using FileUploader.UseCases.FileGroups.FileGroupsGetUploaded.FileGroupsGetUploadedDto;

namespace FileUploader.UseCases.FileGroups;

public class FileGroupsMappingProfile : Profile
{
    public FileGroupsMappingProfile()
    {
        CreateMap<Domain.File, FileDto>();
        CreateMap<FileGroup, FileGroupDto>();
    }
}
