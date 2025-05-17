namespace Totten.Solution.RagnaComercio.ApplicationService.Features.Users.CommandsHandler;
using AutoMapper;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Results;

using MediatR;
using System;
using System.Threading.Tasks;
using Totten.Solution.RagnaComercio.ApplicationService.Features.Users.Commands;
using Totten.Solution.RagnaComercio.Domain.Features.Users;

public class UserDiscordUpdateHandler(IUserRepository userRepository)
    : IRequestHandler<UserDiscordUpdateCommand, Result<Guid>>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<Guid>> Handle(UserDiscordUpdateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userInDb = await _userRepository.GetById(request.UserId);

            return await userInDb.MatchAsync(async user =>
            {
                await _userRepository.Update(user with
                {
                    DiscordUser = request.DiscordUser
                });

                return Result.Of(user.Id);
            }, () => Result.Of<Guid>(NotFoundError.New($"{nameof(User)}: with id '{request.UserId}' not found.")));
        }
        catch (Exception ex)
        {
            return UnhandledError.New($"Error on update user with Id: '{request.UserId}' and DiscordUser: '{request.DiscordUser}'", ex);
        }
    }
}
