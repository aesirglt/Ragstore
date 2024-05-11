namespace Totten.Solution.Ragstore.ApplicationService.Features.Characters.Commands;

using MediatR;
using System;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Unit = Infra.Cross.Functionals.Unit;

public class CharactersCreateCommand : IRequest<Result<Exception, Unit>>
{
}
