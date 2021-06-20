using FastTripApp.DAO.Models.Trip.Way;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastTripApp.DAO.Models.Reports
{
    public class FindNearestPlaces
    {
        public FindNearestPlaces()
        {
            CenterCoords = new Coords();
            SearchRadius = 20;
        }

        [DisplayName("Current position")]
        public Coords CenterCoords { get; set; }
        
        [DisplayName("Search radius")]
        [DisplayFormat(DataFormatString = "{0:0.00}km")]
        public double SearchRadius { get; set; }

        public IEnumerable<NearestPlaces> NearestPlaces { get; set; }
    }
}
