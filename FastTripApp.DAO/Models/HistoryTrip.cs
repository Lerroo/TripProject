using FastTripApp.DAO.Models.StatusEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FastTripApp.DAO.Models
{
    public class HistoryTrip
    {
        [Key]
        public int Id { get; set; }

        public int TripId { get; set; }
        public string Name { get; set; }

        public string Image { get; set; }

        public string Descriprion { get; set; }

        public TimeAfterDeparture TimeAfterDeparture { get; set; }
        
        [DisplayName("Status")]
        public Status StatusEnum { get; set; }
        [NotMapped]
        public string Status { get => StatusEnum.GetStringValue(); }

        public Address Address { get; set; }

        public string UserId { get; set; }
    }
}
