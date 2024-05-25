namespace Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;

using LanguageExt.Common;
using MediatR;
using Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.ResponseModels;

public class ItemByIdQuery : IRequest<Result<ItemDetailResponseModel>>
{
    public int ItemId { get; set; }
    public string Server { get; set; } = string.Empty;
    public string ServerLanguage { get; set; } = string.Empty;
}
