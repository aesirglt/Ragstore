namespace Totten.Solution.Ragstore.ApplicationService.Features.Chats.Queries;

using FunctionalConcepts.Results;using FunctionalConcepts;
using MediatR;
using Totten.Solution.Ragstore.Domain.Features.Chats;
using Totten.Solution.Ragstore.Domain.Features.Servers;

public class ChatCollectionQuery : IRequest<Result<IQueryable<Chat>>>
{
    public Server Server { get; set; }
}
