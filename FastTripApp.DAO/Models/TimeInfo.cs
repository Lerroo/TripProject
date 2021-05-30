﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FastTripApp.DAO.Models
{
    public class TimeInfo
    {
        public TimeInfo()
        {
            var dataError = new DateTime();
            Start = dataError;
            End = dataError;
        }
        [Key]
        public int key { get; set; }
        [DisplayName("Actual start time")]
        public DateTime? Start { get; set; }

        [DisplayName("Actual end time")]
        public DateTime? End { get; set; }

        [DisplayFormat(DataFormatString = "{0:%d}d {0:%h}h {0:%m}m {0:%s}s", ApplyFormatInEditMode = true)]
        [DisplayName("Track time")]
        public TimeSpan? TimeTrack { get => End - Start;}
    }
}
