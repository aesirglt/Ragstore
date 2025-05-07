namespace Totten.Solution.RagnaComercio.ApplicationService.Mappers;
using AutoMapper;
using Totten.Solution.RagnaComercio.ApplicationService.ViewModels.Items;
using Totten.Solution.RagnaComercio.Domain.Features.ItemsAggregation;

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