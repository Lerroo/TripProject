using FastTripApp.DAO.Models;
using FastTripApp.DAO.Repository;
using FastTripApp.DAO.Repository.Interfaces;
using FastTripApp;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace FastTripApp.Controllers
{
    public class TripController : Controller
    {
        private readonly RepositoryTrip _repositoryTrip;
        private readonly RepositoryHistoryTrip _repositoryHistoryTrip;


        public TripController(RepositoryTrip tripRepository, RepositoryHistoryTrip historyRepository)
        {
            _repositoryTrip = tripRepository;
            _repositoryHistoryTrip = historyRepository;
        }

        public void TripInHistory(int id, TimeSpan? time)
        {
            var jobId = BackgroundJob.Schedule(() => DeletePost(id), TimeSpan.FromSeconds(time.Value.Seconds));
            var jobId2 = BackgroundJob.Schedule(() => Response.Redirect("Trip/Index"), TimeSpan.FromSeconds(time.Value.Seconds));
        }

        // GET: TripController
        [Authorize]
        public ActionResult Index()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<Trip> objList = _repositoryTrip.TripsByUserId(id);

            if (objList.GetEnumerator().MoveNext())
            {
                var trip = objList.First();
                //TripInHistory(trip.Id, trip.EstimatedTime);
            }

            return View(objList);
        }

        // GET: TripController/Start/5
        [Authorize]
        public ActionResult Start(int id)
        {
            var obj = new TimeInfo
            {
                Start = Util.TimeNow()
            };

            ViewBag.Id = id;
            return View(obj);
        }

        public ActionResult ToHistory(int id)
        {
            var trip = _repositoryTrip.GetById(id);
            _repositoryHistoryTrip.TripToHistory(trip);
            _repositoryTrip.Delete(id);

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult End(TimeInfo timeInfo, int id)
        {
            timeInfo.End = Util.TimeNow();
            timeInfo.TimeTrack = timeInfo.End - timeInfo.Start;

            var trip = _repositoryTrip.GetById(id);
            trip.TimeInfo = timeInfo;

            _repositoryTrip.Update(trip);
            ToHistory(id);

            return RedirectToRoute(new { controller = "Review", action = "Create" });
        }

        // GET: TripController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TripController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Trip trip)
        {
            if (ModelState.IsValid)
            {
                trip.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _repositoryTrip.Add(trip);
                
                return RedirectToAction("Index");
            }
            return View(trip);
        }

        // GET: TripController/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _repositoryTrip.GetById(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }


        // POST: TripController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Trip obj)
        {
            if (ModelState.IsValid)
            {
                _repositoryTrip.Update(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET: TripController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var trip = _repositoryTrip.GetById(id);
            if (trip == null)
            {
                return NotFound();
            }
            return View(trip);
        }

        // POST: TripController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int? id)
        {
            _repositoryTrip.Delete(id);
            return RedirectToAction("Index");
        }   
    }
}
