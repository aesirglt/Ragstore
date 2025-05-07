namespace Totten.Solution.Ragstore.ApplicationService.Features.Callbacks.Commands;

using FunctionalConcepts;
using FunctionalConcepts.Results;
using MediatR;

public class CallbackSaveCommand : IRequest<Result<Success>>
{
    public int ServerId { get; set; }
    public Guid UserId { get; set; }
    public int ItemId { get; set; }
    public double ItemPrice { get; set; }
}
