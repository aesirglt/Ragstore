namespace Totten.Solution.Ragstore.ApplicationService.Features.Users.Commands;

using FunctionalConcepts.Results;
using MediatR;

public class UserCreateCommand : IRequest<Result<Guid>>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string NormalizedEmail { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
