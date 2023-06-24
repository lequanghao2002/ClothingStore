using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
