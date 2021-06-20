using FastTripApp.DAO.Models.Trip.Way;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FastTripApp.DAO.Models.DTO.Way
{
    public class DefaultWayDTO
    {
        [Key]
        public int WayId { get; set; }

        [Required]
        [DisplayName("Address Start")]
        public Place Start { get; set; }

        [Required]
        [DisplayName("Address End")]
        public Place End { get; set; }

        public string StaticImage { get; set; }
        [NotMapped]
        public string FullStaticImagePath { get => "\\uploads\\way_static_images\\" + StaticImage; }
    }
}
