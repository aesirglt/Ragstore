namespace Totten.Solution.Ragstore.ApplicationService.ViewModels.Users;

public class UserDetailViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string NormalizedEmail { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;
    public string MemberSince { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public long SearchCount { get; set; }
    public bool ReceivePriceAlerts { get; set; }
}
