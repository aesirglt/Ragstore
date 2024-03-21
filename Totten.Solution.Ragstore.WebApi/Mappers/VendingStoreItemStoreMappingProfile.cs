namespace Totten.Solution.Ragstore.WebApi.Mappers;
using AutoMapper;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commands;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Vendings;

/// <summary>
/// 
/// </summary>
public class VendingStoreItemStoreMappingProfile : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public VendingStoreItemStoreMappingProfile()
    {
        CreateMap<VendingStoreItemCommand, VendingStoreItem>();;
    }
}