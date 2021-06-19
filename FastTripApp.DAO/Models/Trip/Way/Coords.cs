using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastTripApp.DAO.Models.Trip.Way
{
    public class Coords
    {
        public Coords()
        {
            Lat = 55.747439;
            Lng = 37.581394;
        }

        [Key]
        public int Id { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
