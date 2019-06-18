using Microsoft.AspNetCore.Mvc;

namespace CameraBazaar2.Web.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}