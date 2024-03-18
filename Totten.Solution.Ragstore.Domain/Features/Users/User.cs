namespace Totten.Solution.Ragstore.Domain.Features.Users;
using Totten.Solution.Ragstore.Domain.Bases;

public class User : Entity<User>
{
    public string Lastname { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Cellphone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
