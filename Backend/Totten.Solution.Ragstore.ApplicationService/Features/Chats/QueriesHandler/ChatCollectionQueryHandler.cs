namespace Totten.Solution.Ragstore.ApplicationService.Features.Chats.QueriesHandler;

using LanguageExt;
using LanguageExt.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Chats.Queries;
using Totten.Solution.Ragstore.Domain.Features.Chats;

public class ChatCollectionQueryHandler : IRequestHandler<ChatCollectionQuery, Result<IQueryable<Chat>>>
{
    private IChatRepository _repository;
    public ChatCollectionQueryHandler(IChatRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<IQueryable<Chat>>> Handle(ChatCollectionQuery request, CancellationToken cancellationToken)
    {
        var chats = await _repository.GetAll(x => x.UpdatedAt >= request.Server.UpdatedAt).AsTask();

        return new Result<IQueryable<Chat>>(chats);
    }
}
