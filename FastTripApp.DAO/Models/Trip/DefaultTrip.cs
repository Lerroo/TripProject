using FastTripApp.DAO.Models.Identity;
using FastTripApp.DAO.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FastTripApp.DAO.Models.Trip.Way;
using FastTripApp.DAO.Models.Review;

namespace FastTripApp.DAO.Models.Trip
{
    public class DefaultTrip
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Descriprion { get; set; }

        public TimeBeforeDeparture TimeBeforeDeparture { get; set; }
        public TimeAfterDeparture TimeAfterDeparture { get; set; }

        public int WayId { get; set; }
        [ForeignKey("WayId")]
        public DefaultWay Way { get; set; }

        [DisplayName("Status")]
        public StatusEnum StatusEnum { get; set; }
        [NotMapped]
        public string Status { get => StatusEnum.GetStringValue(); }

        public string UserId { get; set; }
        public UserCustom User { get; set; }

        #nullable enable
        public int? ReviewId { get; set; }        
        public DefaultReview? Review { get; set; }
        #nullable disable
    }
}
