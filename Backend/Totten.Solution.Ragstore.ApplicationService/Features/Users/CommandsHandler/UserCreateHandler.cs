namespace Totten.Solution.Ragstore.ApplicationService.Features.Users.CommandsHandler;

using AutoMapper;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Users.Commands;
using Totten.Solution.Ragstore.Domain.Features.Users;

public class UserCreateHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<UserCreateCommand, Result<Guid>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<Guid>> Handle(UserCreateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _mapper.Map<User>(request);

            await _userRepository.Save(entity);

            return entity.Id;
        }
        catch (Exception ex)
        {
            return UnhandledError.New($"Error on creating new user with email: '{request.Email}'", ex);
        }
    }
}
