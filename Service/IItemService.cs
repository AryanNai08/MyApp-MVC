using MyApp_MVC.Dtos;
using MyApp_MVC.Models;

namespace MyApp_MVC.Service
{
    public interface IItemService
    {
        Task<List<ItemDto>> GetItemsAsync();
        Task<ItemDto> GetItemById(int id);
        Task<bool> DeleteItem(int id);
        Task<int> SaveItems(SaveItemDto dto);
    }
}
