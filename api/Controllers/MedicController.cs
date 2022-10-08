using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class MedicController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
