using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FastTripApp.DAO.Models.Statistic
{
    public class ObserveTrips
    {
        public TimeSpan? Minimum { get; set; }
        public TimeSpan? Maximum { get; set; }
        public TimeSpan? Average { get; set; }
    }
}
