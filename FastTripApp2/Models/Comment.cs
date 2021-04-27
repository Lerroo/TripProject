using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FastTripApp.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        
        public int UserId { get; set; }
        [Required]
        public int Appraisal { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
