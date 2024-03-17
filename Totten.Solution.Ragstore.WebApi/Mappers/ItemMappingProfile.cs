namespace Totten.Solution.Ragstore.WebApi.Mappers;
using AutoMapper;
using Totten.Solution.Ragstore.Domain.Features.Items;
using Totten.Solution.Ragstore.WebApi.Endpoints.ViewModels.Items;

public class ItemMappingProfile : Profile
{
    public ItemMappingProfile()
    {
        CreateMap<Item, ItemResumeViewModel>();
    }
}