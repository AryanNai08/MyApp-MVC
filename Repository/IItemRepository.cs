using MyApp_MVC.Dtos;
using MyApp_MVC.Models;

namespace MyApp_MVC.Repository
{
    public interface IItemRepository
    {
        Task<List<Item>> ReadItems();
        Task CreateItem(Item item);
        Task UpdateItem(Item item);
        Task<bool> DeleteItem(int id);

        Task<Item> Getitembyid(int id);
    }
}
