namespace Totten.Solution.Ragstore.Infra.Data.Features.Chats;
using Totten.Solution.Ragstore.Domain.Features.Chats;
using Totten.Solution.Ragstore.Infra.Data.Bases;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreServerContext;

public class ChatRepository(ServerStoreContext context)
    : RepositoryBase<Chat>(context), IChatRepository
{

}
