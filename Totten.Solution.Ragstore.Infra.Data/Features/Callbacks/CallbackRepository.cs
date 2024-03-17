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

    public Task<List<Callback>> GetByItemAll(string itemName)
        => _collection
        .Find(cb => cb.Items.ContainsKey(itemName))
        .ToListAsync();

    public async Task<Unit> Save(Callback callback)
    {
        await _collection.InsertOneAsync(callback);
        return new Unit();
    }
}
