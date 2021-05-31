
using FastTripApp.DAO;
using FastTripApp.DAO.Models;
using FastTripApp.DAO.Repository;
using FastTripApp.DAO.Repository.Interfaces;
using FastTripApp.DAO.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace FastTripApp.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly IRepositoryReview _repositoryReview;
        private readonly ITimeAfterDepartureService _timeAfterDepartureService;
        private readonly ITripService _tripService;
        private readonly IUtilService _util;

        public ReviewController(
            IRepositoryReview repositoryReview,
            ITimeAfterDepartureService timeAfterDepartureService,
            ITripService tripService,
            IUtilService util)
        {
            _repositoryReview = repositoryReview;
            _timeAfterDepartureService = timeAfterDepartureService;
            _util = util;
            _tripService = tripService;
        }

        // GET: ReviewController
        public ActionResult Index()
        {
            List<Review> objList = _repositoryReview.GetWithInclude();
            return View(objList);
        }

        public ActionResult GetComments(int id)
        {
            var la = _repositoryReview.GetById(id);
            List<Comment> comments = la.Comments;
            return PartialView("../Comment/_Index", comments);
        }

        // GET: ReviewController/Create
        public ActionResult Create()
        {

            return PartialView("_Create");
        }

        // POST: ReviewController/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Review review)
        {
            if (ModelState.IsValid)
            {
                review.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                review.TimePost = _util.DateTimeNow();

                _repositoryReview.Add(review);
                return RedirectToRoute(new { controller = "Trip", action = "End", id = review.TripId });
            }

            return View("_Create", review);
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
