namespace Totten.Solution.RagnaComercio.ApplicationService.Features.ItemsAggregation.Commands;

using FunctionalConcepts;
using FunctionalConcepts.Results;
using MediatR;

public class ItemCreateCommand : IRequest<Result<Success>>
{
}
