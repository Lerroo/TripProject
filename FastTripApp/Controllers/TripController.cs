﻿using FastTripApp.DAO.Models;
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
using Hangfire.States;
using Hangfire.Storage;
using FastTripApp.DAO.Services;

namespace FastTripApp.Controllers
{
    public class TripController : Controller
    {
        private readonly IRepositoryTrip _repositoryTrip;
        private readonly IRepositoryHistoryTrip _repositoryHistoryTrip;
        private readonly IRepositoryTimeInfo _repositoryTimeInfo;
        private readonly ServiceHangFire _serviceHangFire;


        public TripController(IRepositoryTrip tripRepository, IRepositoryHistoryTrip historyRepository, IRepositoryTimeInfo repositoryTimeInfo)
        {
            _repositoryTrip = tripRepository;
            _repositoryHistoryTrip = historyRepository;
            _repositoryTimeInfo = repositoryTimeInfo;
            _serviceHangFire = new ServiceHangFire();
        }



        // GET: TripController
        [Authorize]
        public ActionResult Index()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<Trip> objList = _repositoryTrip.TripsByUserId(id);

            if (objList.Any())
            {
                var trip = objList.First();
                using (var connection = JobStorage.Current.GetConnection())
                {
                    foreach (var recurringJob in connection.GetRecurringJobs())
                    {
                        RecurringJob.RemoveIfExists(recurringJob.Id);
                    }
                }


                var timeDelay = TimeSpan.FromSeconds(trip.EstimatedTime);
                //var idJob = BackgroundJob.Schedule(() => ToHistory(trip.Id), timeDelay);
                //BackgroundJob.ContinueJobWith(
                //    idJob, () => BackgroundJob.Schedule(() => RedirectToAction("Index"), timeDelay + TimeSpan.FromSeconds(1))
                //    );

            }
            return View(objList);
        }

        // GET: TripController/Start/5
        [Authorize]
        public ActionResult Start(int id)
        {
            var obj = new TimeInfo
            {
                Start = _repositoryTimeInfo.TimeNow()
            };

            ViewBag.Id = id;
            return View(obj);
        }

        //public void ToHistory(int id)
        //{
        //    var trip = _repositoryTrip.GetById(id);
        //    _repositoryHistoryTrip.TripToHistory(trip);
        //    _repositoryTrip.Delete(trip.Id);
        //}


        [HttpPost]
        public ActionResult End(TimeInfo timeInfo, int id)
        {
            var trip = _repositoryTrip.GetById(id);
            trip.TimeInfo = _repositoryTimeInfo.CalculateTime(timeInfo);

            //ToHistory(id);

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
