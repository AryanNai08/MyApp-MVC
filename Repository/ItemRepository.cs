using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyApp_MVC.Data;
using MyApp_MVC.Models;
using System.Data;

namespace MyApp_MVC.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly MyAppDbContext _dbContext;
        private readonly string _connectionString;
        public ItemRepository(MyAppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("defaultconnectionstring");
        }

        private IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);

        public async Task CreateItem(Item item)
        {
            using var conn=CreateConnection();
            var parameters = new
            {
                Name = item.Name,
                Price = item.Price
            };

            await conn.ExecuteAsync("sp_CreateItem", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<Item>> ReadItems()
        {
            using var conn = CreateConnection();
            var result = await conn.QueryAsync<Item>("sp_GetAllItems", commandType: CommandType.StoredProcedure);
           return result.ToList();
        }

        public async Task<Item> Getitembyid(int id)
        {
            using var conn = CreateConnection();
            var result = await conn.QueryFirstOrDefaultAsync<Item>("sp_GetById",new { Id=id }, commandType: CommandType.StoredProcedure);
           return result;
        }

        public async Task UpdateItem(Item item)
        {
            using var conn = CreateConnection();
            var parameters = new
            {
                Id=item.Id,
                Name = item.Name,
                Price = item.Price
            };
            var result = await conn.ExecuteAsync("sp_UpdateItem",parameters, commandType: CommandType.StoredProcedure);
           
        }
        public async Task<bool> DeleteItem(int id)
        {
            using var connection = CreateConnection();

            var affectedRows = await connection.ExecuteAsync("sp_DeleteItem",new { Id = id },commandType: CommandType.StoredProcedure);

            return affectedRows > 0;
        }    

    }
}
