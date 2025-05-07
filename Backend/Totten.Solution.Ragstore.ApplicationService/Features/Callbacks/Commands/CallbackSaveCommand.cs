namespace Totten.Solution.Ragstore.ApplicationService.Features.Callbacks.Commands;

using FunctionalConcepts;
using FunctionalConcepts.Results;
using MediatR;

public class CallbackSaveCommand : IRequest<Result<Success>>
{
    public int ServerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string UserCellphone { get; set; } = string.Empty;
    public int ItemId { get; set; }
    public double ItemPrice { get; set; }
}
