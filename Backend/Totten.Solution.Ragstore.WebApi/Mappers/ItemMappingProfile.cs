namespace Totten.Solution.Ragstore.WebApi.Mappers;
using AutoMapper;
using Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;
using Totten.Solution.Ragstore.WebApi.ViewModels.Items;

/// <summary>
/// 
/// </summary>
public class ItemMappingProfile : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public ItemMappingProfile()
    {
        CreateMap<Item, ItemResumeViewModel>();
    }
}