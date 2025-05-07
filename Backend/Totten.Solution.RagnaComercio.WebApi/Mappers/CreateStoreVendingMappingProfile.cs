namespace Totten.Solution.RagnaComercio.WebApi.Mappers;

using AutoMapper;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Commands;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Commons;
using Totten.Solution.RagnaComercio.WebApi.Dtos.Stores;

/// <summary>
/// 
/// </summary>
public class CreateStoreVendingMappingProfile : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public CreateStoreVendingMappingProfile()
    {
        CreateMap<VendingStoreSaveDto, VendingStoreSaveCommand>();
        CreateMap<VendingStoreItemCommand, VendingStoreItemCommand>();
    }
}