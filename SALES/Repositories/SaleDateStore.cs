using Microsoft.EntityFrameworkCore;
using SALES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALES.Repositories
{
    class SaleDateStore : IDataStore<Sale>
    {
        private readonly ApplicationDbContext dbContext;

        public SaleDateStore(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> AddItemAsync(Sale item)
        {
            await dbContext.Sales.AddAsync(item);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var item = await dbContext.Sales.FirstOrDefaultAsync(x => x.Id == id);
            if (item != null)
            {
                dbContext.Sales.Remove(item);
                await dbContext.SaveChangesAsync();
            }
            return await Task.FromResult(true);
        }

        public async Task<Sale> GetItemAsync(int id)
        {
            var item = await dbContext.Sales.Include(x => x.Employee).Include(x => x.Product).FirstAsync(x => x.Id == id);
            return item;
        }

        public async Task<IEnumerable<Sale>> GetItemsAsync(bool forceRefresh = false)
        {
            var allitems = await dbContext.Sales.Include(x=> x.Employee).Include(x=> x.Product).ToListAsync().ConfigureAwait(false);
            return await Task.FromResult(allitems);
        }

        public async Task<bool> UpdateItemAsync(Sale item)
        {
            dbContext.Update(item);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }
    }
}
