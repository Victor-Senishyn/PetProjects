using Microsoft.AspNetCore.Mvc;

namespace OfficeControlSystemApi.Controllers
{
    public class VisitHistoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
