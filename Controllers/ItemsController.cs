using Microsoft.AspNetCore.Mvc;
using MyApp_MVC.Models;

namespace MyApp_MVC.Controllers
{
    public class ItemsController : Controller
    {
        public IActionResult Overview()
        {
            var item = new Item() { Name = "Aryan" };
            return View(item);
        }
    }
}
