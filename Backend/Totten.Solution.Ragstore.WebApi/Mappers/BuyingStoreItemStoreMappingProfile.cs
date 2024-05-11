namespace Totten.Solution.Ragstore.WebApi.Mappers;
using AutoMapper;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commons;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;

/// <summary>
/// 
/// </summary>
public class BuyingStoreItemStoreMappingProfile : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public BuyingStoreItemStoreMappingProfile()
    {
        CreateMap<BuyingStoreItemCommand, BuyingStoreItem>();
    }
}