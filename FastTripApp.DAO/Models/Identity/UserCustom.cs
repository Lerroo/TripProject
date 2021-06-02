using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastTripApp.DAO.Models.Identity
{
    // Add profile data for application users by adding properties to the UsingIdentityUser class
    public class UserCustom : IdentityUser
    {
        public string Id { get => base.Id; }
        [DisplayName("User name")]
        public string DisplayName { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Firstname { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }
        public List<Review> Reviews { get; set; }
        [DisplayName("Profile photo")]
        public string ImagePath { get; set; }

        [NotMapped]
        public string FullImagePath { get => "/uploads/users/" + Id + "/avatars/" + ImagePath; }
    }
}
