using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FastTripApp2.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        
        public string UserId { get; set; }
        public string UserName { get; set; }
        [Required]
        public int Appraisal { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
