using Microsoft.EntityFrameworkCore;
using SALES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALES.Repositories
{
    class EmployeeDataStore : IDataStore<Employee>
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeDataStore(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> AddItemAsync(Employee item)
        {
            await dbContext.Employees.AddAsync(item);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var item = await dbContext.Employees.FirstAsync(x => x.Id == id);
            if (item != null)
            {
                dbContext.Employees.Remove(item);
                await dbContext.SaveChangesAsync();
            }
            return await Task.FromResult(true);
        }

        public async Task<Employee> GetItemAsync(int id)
        {
            var item = await dbContext.Employees.FirstAsync(x => x.Id == id);
            return item;
        }

        public async Task<IEnumerable<Employee>> GetItemsAsync(bool forceRefresh = false)
        {
            var allitems = await dbContext.Employees.ToListAsync().ConfigureAwait(false);
            return await Task.FromResult(allitems);
        }

        public async Task<bool> UpdateItemAsync(Employee item)
        {
            dbContext.Update(item);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }
    }
}
