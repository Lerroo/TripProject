using FastTripApp.DAO.Models.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastTripApp.DAO.Models.Statistic
{
    public class UserStatistic
    {
        [DisplayName("in")]
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
