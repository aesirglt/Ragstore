namespace Totten.Solution.Ragstore.ApplicationService.Features.Callbacks.Commands;

using MediatR;
using System;
using Totten.Solution.Ragstore.Domain.Features.Callbacks;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Unit = Infra.Cross.Functionals.Unit;
using ItemId = int;
using ItemPrice = double;
public class CallbackSaveCommand : IRequest<Result<Exception, Unit>>
{
    public string Name { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Server { get; set; } = string.Empty;
    public string UserCellphone { get; set; } = string.Empty;
    public Dictionary<ItemId, ItemPrice> Items { get; set; } = new ();
    public ECallbackType Level { get; set; }
}
