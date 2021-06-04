using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FastTripApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: AccountController
        public ActionResult Index()
        {
            return View();
        }
    }
}
