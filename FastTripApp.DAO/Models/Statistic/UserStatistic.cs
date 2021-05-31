using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Models.Statistic
{
    public class UserStatistic
    {
        public int Year { get; set; }
        public Duration Duration { get; set; }
        public Trips Trip { get; set; }
    }
}
