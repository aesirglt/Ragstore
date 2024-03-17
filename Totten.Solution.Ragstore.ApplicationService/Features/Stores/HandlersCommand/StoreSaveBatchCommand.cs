namespace Totten.Solution.Ragstore.ApplicationService.Features.Stores.HandlersCommand;

using MediatR;
using System;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Unit = Infra.Cross.Functionals.Unit;

public class StoreSaveBatchCommand : IRequest<Result<Exception, Unit>>
{
    public IQueryable<StoreSaveCommand> SaveCommands { get; init; }

    public StoreSaveBatchCommand(IQueryable<StoreSaveCommand> storeSaveCommands)
    {
        SaveCommands = storeSaveCommands;
    }
}
