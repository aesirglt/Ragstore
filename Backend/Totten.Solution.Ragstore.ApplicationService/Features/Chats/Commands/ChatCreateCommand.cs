namespace Totten.Solution.Ragstore.ApplicationService.Features.Chats.Commands;

using MediatR;
using System;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Unit = Infra.Cross.Functionals.Unit;

public class ChatCreateCommand : IRequest<Result<Exception, Unit>>
{
}
