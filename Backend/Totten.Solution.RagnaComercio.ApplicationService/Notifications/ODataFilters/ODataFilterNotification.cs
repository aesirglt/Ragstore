namespace Totten.Solution.RagnaComercio.ApplicationService.Notifications.ODataFilters;
using MediatR;

public class ODataFilterNotification : INotification
{
    public string Type { get; set; } = string.Empty;
    public int ItemId { get; set; }
}
