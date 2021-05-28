using FastTripApp.DAO.Models.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastTripApp.DAO.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? TimePost { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int ReviewId { get; set; }
        [ForeignKey("ReviewId")]
        public Review Review { get; set; }
    }
}
