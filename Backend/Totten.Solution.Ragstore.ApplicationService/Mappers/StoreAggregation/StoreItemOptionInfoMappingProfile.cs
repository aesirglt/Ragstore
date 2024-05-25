namespace Totten.Solution.Ragstore.ApplicationService.Mappers.StoreAggregation;

using AutoMapper;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commons;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Bases;
/// <summary>
/// 
/// </summary>
public class StoreItemOptionInfoMappingProfile : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public StoreItemOptionInfoMappingProfile()
    {
        CreateMap<Dictionary<int, InfoOptionStoreItemCommand>, StoreItemOptionInfo[]>()
            .ConstructUsing(e => e.Select(k => new StoreItemOptionInfo(k.Key, k.Value.Val, k.Value.Param, k.Value.Name)).ToArray());
    }
}
