namespace Totten.Solution.Ragstore.ApplicationService.Mappers;
using AutoMapper;
using Totten.Solution.Ragstore.ApplicationService.ViewModels.Items;
using Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;

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