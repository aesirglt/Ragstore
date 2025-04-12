namespace Totten.Solution.Ragstore.WebApi.Mappers;

using AutoMapper;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commands;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commons;
using Totten.Solution.Ragstore.WebApi.Dtos.Stores;

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