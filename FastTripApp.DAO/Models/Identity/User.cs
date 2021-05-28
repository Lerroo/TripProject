using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FastTripApp.DAO.Models.Identity
{
    // Add profile data for application users by adding properties to the UsingIdentityUser class
    public class User : IdentityUser
    {
        [PersonalData]
        [Column(TypeName ="nvarchar(100)")]
        public string Firstname { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }
        public string ProfilePicture { get; set; }

        public List<Review> Reviews { get; set; }
        [DisplayName("Profile photo")]
        public string ImagePath { get; set; }
    }
}
