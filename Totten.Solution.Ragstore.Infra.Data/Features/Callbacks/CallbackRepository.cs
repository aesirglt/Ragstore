namespace Totten.Solution.Ragstore.Infra.Data.Features.Callbacks;

using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.Domain.Features.Callbacks;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class CallbackRepository : ICallbackRepository
{
    private readonly IMongoCollection<Callback> _collection;

    public CallbackRepository(IMongoDatabase mongoDatabase, string collectionName)
    {
        _collection = mongoDatabase.GetCollection<Callback>(collectionName);
    }

    public Task<List<Callback>> GetAll()
        => _collection.Find(_ => true).ToListAsync();

    public Task<List<Callback>> GetByItemAndPriceAll(string itemName, double value)
        => _collection
                .Find(cb => cb.Items.Any(f => f.Key.Contains(itemName) && f.Value <= value))
                .ToListAsync();

    public async Task<Unit> Save(Callback callback)
    {
        await _collection.InsertOneAsync(callback);
        return new Unit();
    }
}
