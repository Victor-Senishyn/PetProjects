using Microsoft.AspNetCore.Mvc;

namespace OfficeControlSystemApi.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
