using FastTripApp.DAO.Models.Identity;
using FastTripApp.Validation;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastTripApp.DAO.Models
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [CheckDateRangeAttribute]
        [DisplayName("Planned start time")]
        public DateTime TimePlain { get; set; }
        
        public long EstimatedTime { get; set; }
        [DisplayName("Estimated time")]
        [DisplayFormat(DataFormatString = "{0:%d}d {0:%h}h {0:%m}m", ApplyFormatInEditMode = true)]
        [NotMapped]
        public TimeSpan EstimatedTimeView
        {
            get  {
                return TimeSpan.FromSeconds(EstimatedTime);
            }
            set
            {
                EstimatedTime = value.Seconds;
            }
        }

        public string Image { get; set; }

        [Required]
        public string Descriprion { get; set; }

        public TimeInfo TimeInfo { get; set; }
                

        [Required]
        [DisplayName("Address Start")]
        public string AddressStart{ get; set; }
        public string AddressStartLatitude { get; set; }
        public string AddressStartLongitude { get; set; }
        [Required]
        [DisplayName("Address End")]
        public string AddressEnd { get; set; }
        public string AddressEndLatitude { get; set; }
        public string AddressEndLongitude { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int? ReviewId { get; set; }
        public Review? Review { get; set; }

    }
}
