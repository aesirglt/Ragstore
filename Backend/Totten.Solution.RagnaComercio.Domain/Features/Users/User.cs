namespace Totten.Solution.RagnaComercio.Domain.Features.Users;

using Totten.Solution.RagnaComercio.Domain.Bases;
using Totten.Solution.RagnaComercio.Domain.Features.CallbackAggregation;

public record User : Entity<User, Guid>
{
    public string Email { get; set; } = string.Empty;
    public string NormalizedEmail { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public long SearchCount { get; set; }
    public bool ReceivePriceAlerts { get; set; }

    public virtual List<Callback> Callbacks { get; set; } = [];
}
