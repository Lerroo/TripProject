
using FastTripApp.DAO;
using FastTripApp.DAO.Models;
using FastTripApp.DAO.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace FastTripApp.Controllers
{
    public class ReviewController : Controller
    {
        private readonly UsingIdentityContext _db;

        public ReviewController(UsingIdentityContext db)
        {
            _db = db;
        }

        // GET: ReviewController
        public ActionResult Index()
        {

            List<Review> objList = _db.Reviews.Include(p=>p.Comments).ToList();

            return View(objList);
        }

        // GET: ReviewController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReviewController/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult AddComment(int idReview)
        {

            return View();
        }

        // POST: ReviewController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Review review)
        {
            if (ModelState.IsValid)
            {
                review.Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                review.TimePost = Util.TimeNow();
                review.User = _db.Users.FromSqlRaw("Select * from AspNetUsers where Id='" + review.Id + "'").First();

                _db.Reviews.Add(review);
                _db.SaveChanges();
                return RedirectToRoute(new { controller = "Trip", action = "Index" });
            }

            return View(review);
        }

        // GET: ReviewController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReviewController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReviewController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReviewController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
