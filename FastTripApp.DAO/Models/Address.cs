using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastTripApp.DAO.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        [Required]
        [DisplayName("Address Start")]
        public string Start { get; set; }
        public string StartCoords { get; set; }
        [Required]
        [DisplayName("Address End")]
        public string End { get; set; }
        public string EndCoords { get; set; }
    }
}
