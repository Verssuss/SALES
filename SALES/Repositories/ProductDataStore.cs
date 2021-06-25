using Microsoft.EntityFrameworkCore;
using SALES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALES.Repositories
{
    class ProductDataStore : IDataStore<Product>
    {
        private readonly ApplicationDbContext dbContext;

        public ProductDataStore(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> AddItemAsync(Product item)
        {
            await dbContext.Products.AddAsync(item);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var item = await dbContext.Products.FirstAsync(x => x.Id == id);
            if (item != null)
            {
                dbContext.Products.Remove(item);
                await dbContext.SaveChangesAsync();
            }
            return await Task.FromResult(true);
        }

        public async Task<Product> GetItemAsync(int id)
        {
            var item = await dbContext.Products.FirstAsync(x => x.Id == id);
            return item;
        }

        public async Task<IEnumerable<Product>> GetItemsAsync(bool forceRefresh = false)
        {
            var allitems = await dbContext.Products.ToListAsync().ConfigureAwait(false);
            return await Task.FromResult(allitems);
        }

        public async Task<bool> UpdateItemAsync(Product item)
        {
            dbContext.Update(item);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }
    }
}
