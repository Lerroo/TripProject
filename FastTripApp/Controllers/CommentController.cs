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

namespace FastTripApp.Controllers
{
    public class CommentController : Controller
    {

        private readonly IRepositoryTimeInfo _repositoryTimeInfo;
        private readonly IRepositoryComment _repositoryComment;

        public CommentController(IRepositoryComment repositoryComment, IRepositoryTimeInfo repositoryTimeInfo)
        {
            _repositoryTimeInfo = repositoryTimeInfo;
            _repositoryComment = repositoryComment;
        }
        // GET: CommentController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CommentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CommentController/Create
        public ActionResult Create(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        // POST: CommentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                comment.TimePost = _repositoryTimeInfo.TimeNow();

                _repositoryComment.Add(comment);
                return RedirectToRoute(new { controller = "Review", action = "Index" });
            }
            return View(comment);
        }

        // GET: CommentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
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

        // GET: CommentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
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
