namespace Totten.Solution.Ragstore.ApplicationService.Mappers;

using AutoMapper;
using Totten.Solution.Ragstore.ApplicationService.Features.Users.Commands;
using Totten.Solution.Ragstore.ApplicationService.ViewModels.Users;
using Totten.Solution.Ragstore.Domain.Features.Users;

/// <summary>
/// 
/// </summary>
public class UserMappingProfile : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public UserMappingProfile()
    {
        CreateMap<User, UserDetailViewModel>()
            .ForMember(ds => ds.MemberSince, m => m.MapFrom(src => src.CreatedAt));

        CreateMap<UserCreateCommand, User>();
    }
}