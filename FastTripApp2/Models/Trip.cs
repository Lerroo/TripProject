using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FastTripApp2.Models
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

        [NotMapped]
        public int SecondsEstimatedTime { get; set; }
        [DisplayName("Estimated time")]
        [DisplayFormat(DataFormatString = "{0:%d}d {0:%h}h {0:%m}m {0:%s}s", ApplyFormatInEditMode = true)]
        public TimeSpan? EstimatedTime
        {
            get  {
                var la = TimeSpan.FromSeconds(SecondsEstimatedTime);
                return la;
            }
            set
            {
                SecondsEstimatedTime = value.Value.Seconds;
            }
        }

        public string Image { get; set; }

        [Required]
        public string Descriprion { get; set; }

        //public int TimeInfoId { get; set; }
        public  TimeInfo TimeInfo { get; set; }
                

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

    }
}
