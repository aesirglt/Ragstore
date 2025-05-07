namespace Totten.Solution.RagnaComercio.ApplicationService.Mappers;
using AutoMapper;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Commons;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Vendings;

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
        CreateMap<VendingStoreItemCommand, VendingStoreItem>()
            .ForMember(ds => ds.CreatedAt, m => m.MapFrom(src => DateTime.UtcNow))
            .ForMember(ds => ds.UpdatedAt, m => m.MapFrom(src => DateTime.UtcNow));
    }
}