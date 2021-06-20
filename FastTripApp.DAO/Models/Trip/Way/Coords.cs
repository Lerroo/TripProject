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
        [DisplayFormat(DataFormatString = "{0,-25:0.00000}")]
        public double Lat { get; set; }
        [DisplayFormat(DataFormatString = "{0,-25:0.00000}")]
        public double Lng { get; set; }

        public override string ToString()
        {
            return $"{Lat}:{Lng}";
        }
    }
}
