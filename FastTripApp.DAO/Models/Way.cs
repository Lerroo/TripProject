using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FastTripApp.DAO.Models
{
    public class Way
    {
        [Key]
        public int AddressId { get; set; }
        [Required]
        [DisplayName("Address Start")]
        public string Start { get; set; }
        public string StartCoords { get; set; }
        [Required]
        [DisplayName("Address End")]
        public string End { get; set; }
        public string EndCoords { get; set; }


        public string StaticImage { get; set; }
        [NotMapped]
        public string FullStaticImagePath { get => "\\uploads\\way_static_images\\" + StaticImage; }
        [NotMapped]
        public string StaticImageUrl { get; set; }
    }
}
