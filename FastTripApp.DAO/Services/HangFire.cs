using FastTripApp.DAO.Models;
using Hangfire;
using Hangfire.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Services
{
    public class ServiceHangFire
    {
        public void ClearJobQueue()
        {
            using (var connection = JobStorage.Current.GetConnection())
            {
                foreach (var recurringJob in connection.GetRecurringJobs())
                {
                    RecurringJob.RemoveIfExists(recurringJob.Id);
                }
            }
        }

        public void AddTripInQueue(Trip trip)
        {
            //var timeDelay = TimeSpan.FromSeconds(trip.EstimatedTime);
            //var idJob = BackgroundJob.Schedule(() => ToHistory(trip.Id), timeDelay);
            //BackgroundJob.ContinueJobWith(
            //    idJob, () => BackgroundJob.Schedule(() => RedirectToAction("Index"), timeDelay + TimeSpan.FromSeconds(1))
            //    );
        }
    }
}
