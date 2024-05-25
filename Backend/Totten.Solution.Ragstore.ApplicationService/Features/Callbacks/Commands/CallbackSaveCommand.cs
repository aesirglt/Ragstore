namespace Totten.Solution.Ragstore.ApplicationService.Features.Callbacks.Commands;

using LanguageExt.Common;
using MediatR;
using Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;
using Unit = LanguageExt.Unit;

public class CallbackSaveCommand : IRequest<Result<Unit>>
{
    public string Name { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Server { get; set; } = string.Empty;
    public string UserCellphone { get; set; } = string.Empty;
    public int ItemId { get; set; }
    public double ItemPrice { get; set; }
    public ECallbackType Level { get; set; }
}
