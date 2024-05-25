namespace Totten.Solution.Ragstore.ApplicationService.Features.Characters.Commands;

using LanguageExt.Common;
using MediatR;
using Unit = LanguageExt.Unit;

public class CharactersCreateCommand : IRequest<Result<Unit>>
{
}
