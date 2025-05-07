namespace Totten.Solution.RagnaComercio.ApplicationService.Features.ItemsAggregation.Queries;

using FunctionalConcepts.Results;
using MediatR;
using Totten.Solution.RagnaComercio.ApplicationService.Features.ItemsAggregation.ResponseModels;

public class ItemByIdQuery : IRequest<Result<ItemDetailResponseModel>>
{
    public int ItemId { get; set; }
    public string Server { get; set; } = string.Empty;
    public string ServerLanguage { get; set; } = string.Empty;
}
