using FastTripApp.DAO.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace FastTripApp.DAO.Models
{
    public class Review 
    {
        [Key]
        public int ReviewId { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "rate the trip")]
        public int? Appraisal { get; set; }
        public DateTime? TimePost { get; set; }
        public List<Comment> Comments { get; set; }
        
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        
    }
}
