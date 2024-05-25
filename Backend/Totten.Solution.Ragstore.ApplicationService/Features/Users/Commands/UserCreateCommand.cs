namespace Totten.Solution.Ragstore.ApplicationService.Features.Users.Commands;

using LanguageExt.Common;
using MediatR;
using Unit = LanguageExt.Unit;

public class UserCreateCommand : IRequest<Result<Unit>>
{
}
