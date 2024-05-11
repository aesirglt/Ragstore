namespace Totten.Solution.Ragstore.ApplicationService.Features.Agents.Queries;

using MediatR;
using System;
using System.Linq;
using Totten.Solution.Ragstore.Domain.Features.AgentAggregation;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class AgentByIdQuery : IRequest<Result<Exception, IQueryable<Agent>>>
{
}
