using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FastTripApp2.Models
{
    public class HistoryTrip
    {
        //public HistoryTrip(Trip trip)
        //{
        //    Id = trip.Id;
        //    Name = trip.Name;
        //    TimePlain = trip.TimePlain;
        //    EstimatedTime = trip.EstimatedTime;
        //    Image = trip.Image;
        //    Descriprion = trip.Descriprion;
        //    StartTrip = trip.StartTrip;
        //    EndTrip = trip.EndTrip;
        //    TimeTrack = trip.TimeTrack;
        //    AddressStart = trip.AddressStart;
        //    AddressEnd = trip.AddressEnd;
        //    AddressEndLatitude = trip.AddressEndLatitude;
        //    AddressEndLongitude = trip.AddressEndLongitude;
        //    AddressStartLatitude = trip.AddressStartLatitude;
        //    AddressStartLongitude = trip.AddressStartLongitude;
        //    UserId = trip.UserId
        //}

        [Key]
        public int Id { get; set; }

        public int TripId { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [CheckDateRangeAttribute]
        [DisplayName("Planned start time")]
        public DateTime TimePlain { get; set; }

        [DisplayName("Estimated time")]
        public TimeSpan? EstimatedTime { get; set; }

        public string Image { get; set; }

        [Required]
        public string Descriprion { get; set; }

        [DisplayName("Actual start time")]
        public DateTime? StartTrip { get; set; }

        [DisplayName("Actual end time")]
        public DateTime? EndTrip { get; set; }

        [DisplayName("Track time")]
        public TimeSpan? TimeTrack { get; set; }

        [Required]
        [DisplayName("Address Start")]
        public string AddressStart { get; set; }
        [Required]
        [DisplayName("Address End")]
        public string AddressEnd { get; set; }

        public string AddressStartLatitude { get; set; }
        public string AddressStartLongitude { get; set; }
        public string AddressEndLatitude { get; set; }
        public string AddressEndLongitude { get; set; }

        public string UserId { get; set; }
    }
}
