namespace Totten.Solution.Ragstore.Domain.Features.Users;
using Totten.Solution.Ragstore.Domain.Bases;

public class User : Entity<User>
{
    public string Lastname { get; set; }
    public string Login { get; set; }
    public string Cellphone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
