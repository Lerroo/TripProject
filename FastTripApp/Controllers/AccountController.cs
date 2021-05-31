using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FastTripApp.Controllers
{
    public class AccountController : Controller
    {
        private IWebHostEnvironment _hostingEnvironment;
        public AccountController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        // GET: AccountController
        public ActionResult Index()
        {
            return View();
        }
    }
}
