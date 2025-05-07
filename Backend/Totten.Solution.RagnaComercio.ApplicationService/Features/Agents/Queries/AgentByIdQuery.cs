namespace Totten.Solution.RagnaComercio.ApplicationService.Features.Agents.Queries;

using FunctionalConcepts.Results;
using MediatR;
using System.Linq;
using Totten.Solution.RagnaComercio.Domain.Features.AgentAggregation;

public class AgentByIdQuery : IRequest<Result<IQueryable<Agent>>>
{
}
