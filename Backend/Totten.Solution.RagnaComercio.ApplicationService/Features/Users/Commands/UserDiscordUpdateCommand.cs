namespace Totten.Solution.RagnaComercio.ApplicationService.Features.Users.Commands;
using FunctionalConcepts.Results;

using MediatR;
using System;

public class UserDiscordUpdateCommand : IRequest<Result<Guid>>
{
    public Guid UserId { get; set; }
    public string DiscordUser { get; set; } = string.Empty;
}
