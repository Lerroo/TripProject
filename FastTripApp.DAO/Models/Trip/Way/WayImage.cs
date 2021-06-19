using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FastTripApp.DAO.Models.Trip.Way
{
    public class WayImage
    {
        public string StaticImage { get; set; }
        [NotMapped]
        public string FullStaticImagePath { get => "/uploads/way_static_images/" + StaticImage; }
    }
}
