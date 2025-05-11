namespace Totten.Solution.RagnaComercio.ApplicationService.Mappers.StoreAggregation;

using AutoMapper;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Commons;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.ResponseModels;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Vendings;

/// <summary>
/// 
/// </summary>
public class VendingStoreItemMappingProfile : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public VendingStoreItemMappingProfile()
    {
        CreateMap<VendingStoreItemCommand, VendingStoreItem>()
            .ForMember(ds => ds.CreatedAt, m => m.MapFrom(src => DateTime.UtcNow))
            .ForMember(ds => ds.UpdatedAt, m => m.MapFrom(src => DateTime.UtcNow))
            .ForMember(ds => ds.InfoCards, m => m.MapFrom(src => src.InfoCards))
            .ForMember(ds => ds.InfoOptions, m => m.MapFrom(src => src.InfoOptions));

        CreateMap<StoreItemResumeViewModel, StoreItemResumeViewModel>();
    }
}
