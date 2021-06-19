using FastTripApp.DAO.Models.Trip.Way;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTripApp.DAO.Models.Reports
{
    public class NearestPlace
    {
        public Coords CenterCoords { get; set; }
        
        public double RadiusDistance { get; set; } //km

        public IEnumerable<Place> Places { get; set; }
    }
}
