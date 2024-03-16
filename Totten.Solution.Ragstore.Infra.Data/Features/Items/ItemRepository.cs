namespace Totten.Solution.Ragstore.Infra.Data.Features.Items;

using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.Domain.Features.Items;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class ItemRepository : IItemRepository
{
    private readonly IMongoCollection<Item> _collection;

    public ItemRepository(IMongoDatabase mongoDatabase, string collectionName)
    {
        _collection = mongoDatabase.GetCollection<Item>(collectionName);
    }

    public Task<List<Item>> GetAll()
    {
        return _collection.Find(_ => true).ToListAsync();
    }

    public async Task<Unit> Save(Item store)
    {
        await _collection.InsertOneAsync(store);
        return new Unit();
    }
}
