namespace Totten.Solution.Ragstore.WebApi.Mappers;
using AutoMapper;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commons;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;

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
        CreateMap<VendingStoreItemCommand, VendingStoreItem>();
    }
}