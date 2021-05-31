using FastTripApp.DAO.Models.Identity;
using FastTripApp.DAO.Repository;
using FastTripApp.DAO.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FastTripApp.DAO.Models.Statistic
{
    public class UserStatistic
    {
        [DisplayName("for")]
        public IEnumerable<SelectListItem> Years { get; set; }
        [NotMapped]
        public int Year { get; set; }
        [DisplayName("Observe")]
        public ObserveTrips ObserveTrips { get; set; }
        [DisplayName("Count")]
        public CountTrips CountTrips { get; set; }
        [DisplayName("Location")]
        public LocationsTrips LocationsTrips { get; set; }
        public HistoryTrip LastTrip { get; set; }
        public User User { get; set; }
    }
}
