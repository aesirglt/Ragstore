namespace Totten.Solution.Ragstore.WebApi.Mappers.StoreAggregation;

using AutoMapper;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Bases;

public class StoreItemCardInfoMappingProfile : Profile
{
    public StoreItemCardInfoMappingProfile()
    {

        CreateMap<Dictionary<int, string>, StoreItemCardInfo[]>()
            .ConstructUsing(e => e.Select(k => new StoreItemCardInfo(k.Key, k.Value)).ToArray());
    }
}
