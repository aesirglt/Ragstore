namespace Totten.Solution.Ragstore.WebApi.Mappers.StoreAggregation;

using AutoMapper;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commands;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Bases;

public class StoreItemOptionInfoMappingProfile : Profile
{
    public StoreItemOptionInfoMappingProfile()
    {
        CreateMap<Dictionary<int, InfoOptionStoreItemCommand>, StoreItemOptionInfo[]>()
            .ConstructUsing(e => e.Select(k => new StoreItemOptionInfo(k.Key, k.Value.Val, k.Value.Param, k.Value.Name)).ToArray());
    }
}
