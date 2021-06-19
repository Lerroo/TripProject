
using FastTripApp.DAO.Models.Enums;
using FastTripApp.DAO.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FastTripApp.DAO.Models.Trip;
using FastTripApp.DAO.Models.Trip.Way;

namespace FastTripApp.DAO.Models
{
    public class HistoryTrip
    {
        [Key]
        public int Id { get; set; }

        public int TripId { get; set; }
        public string Name { get; set; }
        public string Descriprion { get; set; }

        public TimeAfterDeparture TimeAfterDeparture { get; set; }
        
        [DisplayName("Status")]
        public StatusEnum StatusEnum { get; set; }
        [NotMapped]
        public string Status { get => StatusEnum.GetStringValue(); }

        public DefaultWay Way { get; set; }

        public string UserId { get; set; }
    }
}
