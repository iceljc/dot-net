using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IPurchaseRepository: IAsyncRepository<Purchase>
    {
        Task<IEnumerable<Purchase>> GetAllPurchases(int pageSize = 30, int pageIndex = 0);
        Task<IEnumerable<Purchase>> GetAllPurchasesByMovie(int movieId, int pageSize = 30, int pageIndex = 0);
    }
}