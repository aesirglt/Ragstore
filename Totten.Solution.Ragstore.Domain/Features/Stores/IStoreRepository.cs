namespace Totten.Solution.Ragstore.Domain.Features.Stores;
using System.Collections.Generic;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public interface IStoreRepository
{
    Task<Store> GetById(string id);
    Task<List<Store>> GetAll();
    Task<Unit> Save(Store store);
}
