using FastTripApp.DAO.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastTripApp.DAO.Models.Review
{
    public class DefaultReview 
    {
        [Key]
        public int ReviewId { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "rate the trip")]
        public int? Appraisal { get; set; }
        public DateTime? TimePost { get; set; }
        public List<Comment> Comments { get; set; }

        public int TripId { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public UserCustom User { get; set; }
        
    }
}
