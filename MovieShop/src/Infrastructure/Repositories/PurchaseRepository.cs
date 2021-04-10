using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PurchaseRepository : EfRepository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Purchase>> GetAllPurchases(int pageSize = 30, int pageIndex = 1)
        {
            var purchases = await _dbContext.Purchases.Include(m => m.Movie).OrderByDescending(p => p.PurchaseDateTime)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return purchases;
        }

        public async Task<IEnumerable<Purchase>> GetAllPurchasesByMovie(int movieId, int pageSize = 30,
            int pageIndex = 1)
        {
            var purchases = await _dbContext.Purchases.Where(p => p.MovieId == movieId).Include(m => m.Movie)
                .Include(m => m.Customer)
                .OrderByDescending(p => p.PurchaseDateTime)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return purchases;
        }
    }
}