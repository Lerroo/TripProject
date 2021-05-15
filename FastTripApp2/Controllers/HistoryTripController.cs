
using FastTripApp2.Models;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UsingIdentity.Data;

namespace FastTripApp2.Controllers
{
    public class HistoryTripController : Controller
    {
        private readonly UsingIdentityContext _db;
        public HistoryTripController(UsingIdentityContext db)
        {
            _db = db;
        }

        [Authorize]
        public ActionResult Index()
        {
            IEnumerable<HistoryTrip> objList = _db.HistoryTrips;
            return View(objList);
        }
    }
}
