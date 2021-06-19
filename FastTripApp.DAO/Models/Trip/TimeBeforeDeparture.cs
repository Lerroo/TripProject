using FastTripApp.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FastTripApp.DAO.Models.Trip
{
    public class TimeBeforeDeparture
    {
        [Key]
        public int Id { get; set; }

        [CheckDateRangeAttribute]
        [DisplayName("Planned start time")]
        public DateTime? ApproximateStart { get; set; }

        public long Estimated { get; set; }

        [DisplayName("Estimated time")]
        [DisplayFormat(DataFormatString = "{0:%d}d {0:%h}h {0:%m}m", ApplyFormatInEditMode = true)]
        [NotMapped]
        public TimeSpan EstimatedView
        {
            get
            {
                return TimeSpan.FromSeconds(Estimated);
            }
            set
            {
                Estimated = value.Seconds;
            }
        }
    }
}
