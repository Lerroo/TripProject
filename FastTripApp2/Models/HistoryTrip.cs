﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FastTripApp2.Models
{
    public class HistoryTrip : Trip
    {
        
        [Key]
        public int Id { get; set; }

        public int TripId { get; set; }
        public string Name { get; set; }

        [DisplayName("Planned start time")]
        public DateTime TimePlain { get; set; }

        [DisplayName("Estimated time")]
        public TimeSpan? EstimatedTime { get; set; }

        public string Image { get; set; }

        public string Descriprion { get; set; }

        [DisplayName("Actual start time")]
        public DateTime? StartTrip { get; set; }

        [DisplayName("Actual end time")]
        public DateTime? EndTrip { get; set; }

        [DisplayFormat(DataFormatString = "{0:%d}d {0:%h}h {0:%m}m {0:%s}s", ApplyFormatInEditMode = true)]
        [DisplayName("Track time")]        
        public TimeSpan? TimeTrack { get; set; }

        [DisplayName("Address Start")]
        public string AddressStart { get; set; }
        [DisplayName("Address End")]
        public string AddressEnd { get; set; }

        public string AddressStartLatitude { get; set; }
        public string AddressStartLongitude { get; set; }
        public string AddressEndLatitude { get; set; }
        public string AddressEndLongitude { get; set; }

        public string UserId { get; set; }
    }
}
