namespace Totten.Solution.Ragstore.ApplicationService.Features.Chats.CommandsHandler;

using FunctionalConcepts.Results;using FunctionalConcepts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Chats.Commands;


public class ChatCreateCommandHandler : IRequestHandler<ChatCreateCommand, Result<Success>>
{
    public Task<Result<Success>> Handle(ChatCreateCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
