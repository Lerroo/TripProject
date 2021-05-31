using FastTripApp.DAO;
using FastTripApp.DAO.Models;
using FastTripApp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FastTripApp.DAO.Repository;
using FastTripApp.DAO.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using FastTripApp.DAO.Services.Interfaces;

namespace FastTripApp.Controllers
{
    public class CommentController : Controller
    {
        private readonly IRepositoryTimeAfterDeparture _repositoryTimeInfo;
        private readonly IRepositoryComment _repositoryComment;
        private readonly IUtilService _util;

        public CommentController(
            IRepositoryComment repositoryComment, 
            IRepositoryTimeAfterDeparture repositoryTimeInfo,
            IUtilService util)
        {
            _repositoryTimeInfo = repositoryTimeInfo;
            _repositoryComment = repositoryComment;
            _util = util;
        }
        // GET: CommentController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreatePartual()
        {
            return View();
        }

        //// GET: CommentController/Create
        //public ActionResult Create(int id)
        //{
        //    ViewBag.Id = id;
        //    return View();
        //}

        // POST: CommentController/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                comment.TimePost = _util.DateTimeNow();

                _repositoryComment.Add(comment);
                return RedirectToRoute(new { controller = "Review", action = "Index" });
            }
            return View(comment);
        }

        // POST: CommentController/Edit/5
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

        // POST: CommentController/Delete/5
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
