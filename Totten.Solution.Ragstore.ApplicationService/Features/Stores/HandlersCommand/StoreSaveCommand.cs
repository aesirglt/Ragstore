namespace Totten.Solution.Ragstore.ApplicationService.Features.Stores.HandlersCommand;

using MediatR;
using System;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Unit = Infra.Cross.Functionals.Unit;

public class StoreSaveCommand : IRequest<Result<Exception, Unit>>
{
    public string Server { get; set; } = string.Empty;
    public string SellerName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public Dictionary<string, double> Items { get; set; } = new Dictionary<string, double>();
    public DateTime CreationDate { get; set; }
}
