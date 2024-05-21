namespace Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.QueriesHandler;

using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;
using Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.ResponseModels;
using Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class ItemByIdQueryHandler : IRequestHandler<ItemByIdQuery, Result<Exception, ItemDetailResponseModel>>
{
    private IMapper _mapper;
    private IItemRepository _itemRepository;
    private HttpClient _client;

    public ItemByIdQueryHandler(IMapper mapper, IItemRepository itemRepository, IHttpClientFactory clientFactory)
    {
        _mapper = mapper;
        _itemRepository = itemRepository;
        _client = clientFactory.CreateClient("ItemDB");
    }

    public async Task<Result<Exception, ItemDetailResponseModel>> Handle(ItemByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetById(request.ItemId);

        var searchItemDetail = async () =>
        {
            var response = await _client.GetAsync("", cancellationToken);
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ItemDetailResponseModel>(content) ?? new ItemDetailResponseModel
            {
                ItemId = item?.Id ?? request.ItemId,
                Name = item?.Name ?? ""
            };
        };

        return item == null
                ? new Exception("Item not found")
                : await searchItemDetail();
    }
}
