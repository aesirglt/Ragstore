namespace Totten.Solution.Ragstore.WebApi.Controllers;

using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commands;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Queries;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.WebApi.Bases;
using Totten.Solution.Ragstore.WebApi.ViewModels.Stores;

[ApiController]
[Route("[controller]")]
public class StoresController : BaseApiController
{
    public StoresController(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }

    [HttpGet()]
    public async Task<IActionResult> GetAll(
        [FromQuery] string server,
        ODataQueryOptions<StoreResumeViewModel> queryOptions)
            => await HandleQueryable(new StoreCollectionQuery(), server, queryOptions);

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(
        [FromQuery] string server,
        [FromRoute] int id)
            => await HandleQuery<VendingStore, StoreDetailViewModel>(
                        new StoreByIdQuery(id),
                        server);
    [HttpPost()]
    public async Task<IActionResult> Post(
        [FromQuery] string server,
        [FromBody] VendingStoreSaveCommand createCmd)
            => await HandleCommand(createCmd, server);

    [HttpPost("batch")]
    public async Task<IActionResult> PostBatch(
        [FromQuery] string server,
        [FromBody] VendingStoreSaveCommand[] createCmd)
           => await Task.FromResult(HandleAccepted(server, createCmd));
}
