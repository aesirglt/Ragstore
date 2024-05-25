namespace Totten.Solution.Ragstore.ApplicationService.Features.Chats.Commands;

using LanguageExt.Common;
using MediatR;
using Unit = LanguageExt.Unit;

public class ChatCreateCommand : IRequest<Result<Unit>>
{
}
