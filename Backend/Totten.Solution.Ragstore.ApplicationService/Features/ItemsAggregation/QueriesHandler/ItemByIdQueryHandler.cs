namespace Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.QueriesHandler;

using Autofac;
using AutoMapper;
using LanguageExt.Common;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Queries;
using Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.ResponseModels;
using Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;
using Totten.Solution.Ragstore.Infra.Cross.Statics;

public class ItemByIdQueryHandler : IRequestHandler<ItemByIdQuery, Result<ItemDetailResponseModel>>
{
    private IMapper _mapper;
    private IItemRepository _itemRepository;
    private HttpClient _client;
    private Dictionary<string, string> _queries;

    public ItemByIdQueryHandler(
        IMapper mapper,
        IItemRepository itemRepository,
        IComponentContext clientFactory)
    {
        _mapper = mapper;
        _itemRepository = itemRepository;
        _client = clientFactory.ResolveNamed<HttpClient>("ApiItemDB");
        _queries = clientFactory.ResolveNamed<Dictionary<string, string>>("ApiItemDB");
    }

    public async Task<Result<ItemDetailResponseModel>> Handle(ItemByIdQuery query, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetById(query.ItemId);

        var searchItemDetail = async () =>
        {
            var server = DefaultServerList.Servers.FirstOrDefault(x => x.Equals(query.Server)) ?? "jRO";

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("RagnaStoreAPI", "v1")));
            _client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(query.ServerLanguage ?? "pt-BR"));

            var queryString = string.Join('&', _queries.Select(d => $"{d.Key}={d.Value}"));
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"api/database/Item/{query.ItemId}?{queryString}");

            var response = await _client.SendAsync(requestMessage, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var detail = JsonConvert.DeserializeObject<ItemDetailResponseModel>(content) ?? new ItemDetailResponseModel
                {
                    Id = query.ItemId
                };

                return detail with
                {
                    ItemUrlImage = $"https://static.divine-pride.net/images/items/item/{query.ItemId}.png"
                };;
            }

            return new Result<ItemDetailResponseModel>(new Exception("not found item"));
        };

        return item == null
                ? new Result<ItemDetailResponseModel>( new Exception("Item not found"))
                : await searchItemDetail();
    }
}
