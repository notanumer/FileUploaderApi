using AutoMapper;
using FileUploader.Domain;
using FileUploader.UseCases.Users.RegisterUser;

namespace FileUploader.UseCases.Users;

public class UsersMappingProfile : Profile
{
    public UsersMappingProfile()
    {
        CreateMap<RegisterUserCommand, User>()
            .ForMember(u => u.PasswordHash, dest => dest.Ignore())
            .ForMember(u => u.PasswordSalt, dest => dest.Ignore())
            .ForMember(u => u.FileGroups, dest => dest.Ignore())
            .ForMember(u => u.Id, dest => dest.Ignore());
    }
}
