namespace Totten.Solution.RagnaComercio.ApplicationService.Mappers.StoreAggregation;
using AutoMapper;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Commons;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Buyings;

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
        CreateMap<BuyingStoreItemCommand, BuyingStoreItem>()
            .ForMember(ds => ds.CreatedAt, m => m.MapFrom(src => DateTime.UtcNow))
            .ForMember(ds => ds.UpdatedAt, m => m.MapFrom(src => DateTime.UtcNow));
    }
}