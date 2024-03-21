namespace Totten.Solution.Ragstore.Domain.Features.StoresAgreggation;
using System.Collections.Generic;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Vendings;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public interface IStoreRepository
{
    Task<VendingStore> GetById(Guid id);
    Task<List<VendingStore>> GetAll();
    Task<Unit> Save(VendingStore store);
    Task<Unit> SaveBatch(IQueryable<VendingStore> stores);
}
