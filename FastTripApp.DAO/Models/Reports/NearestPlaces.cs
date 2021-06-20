using FastTripApp.DAO.Models.Trip.Way;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastTripApp.DAO.Models.Reports
{
    public class NearestPlaces
    {
        public Place Place { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.00} km")]
        public double Distance { get; set; }
    }
}
