namespace Totten.Solution.Ragstore.ApplicationService.Features.Chats.CommandsHandler;

using LanguageExt.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Chats.Commands;
using Unit = LanguageExt.Unit;

public class ChatCreateCommandHandler : IRequestHandler<ChatCreateCommand, Result<Unit>>
{
    public Task<Result<Unit>> Handle(ChatCreateCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
