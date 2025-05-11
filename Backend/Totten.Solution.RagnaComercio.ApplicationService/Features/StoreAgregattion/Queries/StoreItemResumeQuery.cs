namespace Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.Queries;

using FunctionalConcepts.Results;
using MediatR;
using Totten.Solution.RagnaComercio.ApplicationService.Features.StoreAgregattion.ResponseModels;

public class StoreItemResumeQuery : IRequest<Result<IQueryable<StoreItemResumeViewModel>>>
{
    public required string StoreType { get; set; }
}