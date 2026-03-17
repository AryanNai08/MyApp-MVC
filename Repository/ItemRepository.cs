using Microsoft.EntityFrameworkCore;
using MyApp_MVC.Data;
using MyApp_MVC.Dtos;
using MyApp_MVC.Models;

namespace MyApp_MVC.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly MyAppDbContext _dbContext;
        public ItemRepository(MyAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateItem(Item item)
        {
           await _dbContext.Items.AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteItem(int id)
        {
            var item=await _dbContext.Items.FindAsync(id);
             _dbContext.Items.Remove(item);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Item> Getitembyid(int id)
        {
            return await _dbContext.Items.FirstOrDefaultAsync(i=>i.Id==id);
        }

        public async Task<List<Item>> ReadItems()
        {
            return await _dbContext.Items.ToListAsync();
        }

        public async Task UpdateItem(Item item)
        {
            _dbContext.Items.Update(item);
            await _dbContext.SaveChangesAsync();
        }
    }
}
