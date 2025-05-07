namespace Totten.Solution.RagnaComercio.ApplicationService.Features.Chats.Queries;

using FunctionalConcepts.Results;
using MediatR;
using Totten.Solution.RagnaComercio.Domain.Features.Chats;
using Totten.Solution.RagnaComercio.Domain.Features.Servers;

public class ChatCollectionQuery : IRequest<Result<IQueryable<Chat>>>
{
    public Server Server { get; set; }
}
