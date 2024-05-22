namespace Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;

using MediatR;
using Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.ResponseModels;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class ItemByIdQuery : IRequest<Result<Exception, ItemDetailResponseModel>>
{
    public int ItemId { get; set; }
    public string Server { get; set; } = string.Empty;
    public string ServerLanguage { get; set; } = string.Empty;
}
