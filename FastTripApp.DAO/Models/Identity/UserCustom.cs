using FastTripApp.DAO.Models.Review;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastTripApp.DAO.Models.Identity
{

    public class UserCustom : IdentityUser
    {
        public string UserId { get; set; }
        [DisplayName("User name")]
        public string DisplayName { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Firstname { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }
        public List<DefaultReview> Reviews { get; set; }
        [DisplayName("Profile photo")]
        public string ProfilePhoto { get; set; }

        [NotMapped]
        public string FullImagePath { get {
                if (ProfilePhoto != "defaultProfilePhoto.png")
                {
                   return "/uploads/users/" + Id + "/avatars/" + ProfilePhoto;
                }
                return "/uploads/defaultProfilePhoto.png";
            } }
    }
}
