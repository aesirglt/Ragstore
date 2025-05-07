namespace Totten.Solution.RagnaComercio.ApplicationService.Mappers;

using AutoMapper;
using Totten.Solution.RagnaComercio.ApplicationService.Features.Users.Commands;
using Totten.Solution.RagnaComercio.ApplicationService.ViewModels.Users;
using Totten.Solution.RagnaComercio.Domain.Features.Users;

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