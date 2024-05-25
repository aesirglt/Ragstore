namespace Totten.Solution.Ragstore.ApplicationService.Features.Agents.Queries;

using LanguageExt.Common;
using MediatR;
using System.Linq;
using Totten.Solution.Ragstore.Domain.Features.AgentAggregation;

public class AgentByIdQuery : IRequest<Result<IQueryable<Agent>>>
{
}
