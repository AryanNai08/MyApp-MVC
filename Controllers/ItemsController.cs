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
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _itemService.GetItemsAsync();
            return View(items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateItemDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _itemService.CreateItemAsync(dto);
            return RedirectToAction("GetAllItems");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var items = await _itemService.GetItemsAsync();
            var item = items.FirstOrDefault(i => i.Id == id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ItemDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _itemService.UpdateItem(dto.Id, dto);

            if (!result)
                return NotFound();

            return RedirectToAction("GetAllItems");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var items = await _itemService.GetItemsAsync();
            var item = items.FirstOrDefault(i => i.Id == id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _itemService.DeleteItem(id);

            if (!result)
                return NotFound();

            return RedirectToAction("GetAllItems");
        }
    }
}