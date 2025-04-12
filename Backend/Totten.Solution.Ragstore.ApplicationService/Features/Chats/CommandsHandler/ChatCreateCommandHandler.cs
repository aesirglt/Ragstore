namespace Totten.Solution.Ragstore.ApplicationService.Features.Chats.CommandsHandler;

using AutoMapper;
using FunctionalConcepts;
using FunctionalConcepts.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Chats.Commands;
using Totten.Solution.Ragstore.Domain.Features.Chats;

public class ChatCreateCommandHandler(IChatRepository chatRepository, IMapper mapper) : IRequestHandler<ChatCreateCommand, Result<Success>>
{
    private readonly IChatRepository _chatRepository = chatRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<Success>> Handle(ChatCreateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var chat = _mapper.Map<Chat>(request);
            return await _chatRepository.Save(chat);
        }
        catch (Exception ex)
        {
            return Result.Success;
        }
    }
}
