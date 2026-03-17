using MyApp_MVC.Dtos;
using MyApp_MVC.Models;

namespace MyApp_MVC.Service
{
    public interface IItemService
    {
        Task<List<ItemDto>> GetItemsAsync();
        Task<ItemDto>CreateItemAsync(CreateItemDto dto);

        Task<bool> UpdateItem(int id, ItemDto dto);
        Task<bool> DeleteItem(int id);
    }
}
