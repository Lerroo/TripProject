using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FastTripApp.DAO.Models
{
    public class TimeAfterDeparture
    {
        /// <summary>
        /// Set Start and End time =  DateTime.Now;
        /// </summary>
        public TimeAfterDeparture()
        {
            var currentTime = DateTime.Now;
            Start = currentTime;
            End = currentTime;
        }
        [Key]
        public int Id { get; set; }
        [DisplayName("Actual start time")]
        public DateTime? Start { get; set; }

        [DisplayName("Actual end time")]
        public DateTime? End { get; set; }

        [DisplayFormat(DataFormatString = "{0:%d}d {0:%h}h {0:%m}m {0:%s}s", ApplyFormatInEditMode = true)]
        [DisplayName("Track time")]
        public TimeSpan? Observe { get => End - Start;}
    }
}
