namespace Totten.Solution.RagnaComercio.ApplicationService.Features.ItemsAggregation.Queries;

using FunctionalConcepts.Results;
using MediatR;
using Totten.Solution.RagnaComercio.Domain.Features.ItemsAggregation;

public class ItemCollectionQuery : IRequest<Result<IQueryable<Item>>>;