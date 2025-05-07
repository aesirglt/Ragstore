namespace Totten.Solution.RagnaComercio.ApplicationService.Features.Chats.QueriesHandler;

using FunctionalConcepts.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.ApplicationService.Features.Chats.Queries;
using Totten.Solution.RagnaComercio.Domain.Features.Chats;
using Totten.Solution.RagnaComercio.Infra.Cross.Statics;

public class ChatCollectionQueryHandler(IChatRepository repository) : IRequestHandler<ChatCollectionQuery, Result<IQueryable<Chat>>>
{
    private readonly IChatRepository _repository = repository;

    public async Task<Result<IQueryable<Chat>>> Handle(ChatCollectionQuery request, CancellationToken cancellationToken)
    {
        var chats = await _repository.GetAll(x => x.UpdatedAt >= request.Server.UpdatedAt).AsTask();

        return Result.Of(chats);
    }
}
