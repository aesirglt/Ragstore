﻿namespace Totten.Solution.RagnaComercio.ApplicationService.Mappers.StoreAggregation;

using AutoMapper;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Commons;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Bases;
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
