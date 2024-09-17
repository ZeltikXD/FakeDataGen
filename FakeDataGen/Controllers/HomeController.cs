using FakeDataGen.Code;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FakeDataGen.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["LocaleList"] = new SelectList(StaticData._locales, "Value", "Name", Locale.EN);
            return View();
        }
    }
}
