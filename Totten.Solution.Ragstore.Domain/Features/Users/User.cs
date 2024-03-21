namespace Totten.Solution.Ragstore.Domain.Features.Users;

using Totten.Solution.Ragstore.Domain.Bases;

public class User : Entity<User, int>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    //public string MonitorItem MyProperty { get; set; }
    //public string SearchLog MyProperty { get; set; }
}
