namespace Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Commands;

using MediatR;
using System;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Unit = Infra.Cross.Functionals.Unit;

public class ItemCreateCommand : IRequest<Result<Exception, Unit>>
{
}
