using Microsoft.AspNetCore.Mvc;
using MyApp_MVC.Dtos;
using MyApp_MVC.Service;

namespace MyApp_MVC.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _itemService.GetItemById(id);
            
            return Ok(item);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _itemService.GetItemsAsync();
            return Ok(items);
        }
                
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _itemService.DeleteItem(id);

            return Ok(new
            {
                success = true,
                message = "Item deleted successfully!"

            });
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] SaveItemDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Validation failed"
                });
            }

            var id = await _itemService.SaveItems(dto);

            return Ok(new
            {
                success = true,
                message = dto.Id == null || dto.Id == 0
                            ? "Item created successfully!"
                            : "Item updated successfully!",
                id = id
            });
        }
    }
}