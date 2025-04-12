namespace Totten.Solution.Ragstore.ApplicationService.Features.Agents.Queries;
using FunctionalConcepts.Results;

using MediatR;
using System.Linq;
using Totten.Solution.Ragstore.Domain.Features.AgentAggregation;

public class AgentCollectionQuery : IRequest<Result<IQueryable<Agent>>>
{
    public int ServerId { get; set; }
}
