using Microsoft.AspNetCore.Mvc;

namespace Rap_Blog.Web.Areas.UserPannel.Conrollers
{
    [Area("UserPannel")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
