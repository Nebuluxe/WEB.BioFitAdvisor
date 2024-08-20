using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEB.BioFitAdvisor.Models;

namespace WEB.BioFitAdvisor.Controllers
{
    public class WebSiteController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public WebSiteController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Auth()
        {
            return View("~/WebSite/Login/Auth.cshtml");
        }
        public IActionResult index()
        {
            return View("~/WebSite/index.cshtml");
        }

        public IActionResult about()
        {
            return View("~/WebSite/about.cshtml");
        }

        public IActionResult blog()
        {
            return View("~/WebSite/blog.cshtml");
        }

        public IActionResult blogDetails()
        {
            return View("~/WebSite/blogDetails.cshtml");
        }

        public IActionResult contact()
        {
            return View("~/WebSite/contact.cshtml");
        }

        public IActionResult index2()
        {
            return View("~/WebSite/index2.cshtml");
        }

        public IActionResult updates()
        {
            return View("~/WebSite/updates.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}