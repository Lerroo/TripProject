using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastTripApp.DAO.Models.Trip.Way
{
    public class DefaultWay
    {
        [Key]
        public int AddressId { get; set; }

        [Required]
        [DisplayName("Address Start")]
        public Place Start { get; set; }

        public int EndPlaceId { get; set; }
        [Required]
        [DisplayName("Address End")]
        public Place End { get; set; }

        public string StaticImage { get; set; }
        [NotMapped]
        public string FullStaticImagePath { get => "\\uploads\\way_static_images\\" + StaticImage; }
        [NotMapped]
        public string StaticImageUrl { get; set; }
    }
}
