namespace Totten.Solution.Ragstore.Infra.Cross.CrossDTOs;

public enum EUserLevel
{
    None = 0,
    VIP1,
    VIP2,
    AGENT,
    SYSTEM
}

public class UserData
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Cellphone { get; set; } = string.Empty;
    public EUserLevel Level { get; set; }
}
