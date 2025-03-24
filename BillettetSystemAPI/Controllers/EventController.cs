using Microsoft.AspNetCore.Mvc;

namespace BillettetSystemAPI.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
