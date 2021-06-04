using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PCPartForum.Models
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/aspnet/core/security/authentication/add-user-data?view=aspnetcore-5.0&tabs=visual-studio
    /// explains how to refactor for anything using IdentityUser 
    /// </summary>
    public class UserProfile : IdentityUser
    {
        public string Bio { get; set; }

        [NotMapped]
        [Display(Name = "Profile Picture")]
        public IFormFile ProfilePicture { get; set; }

        public string PhotoUrl { get; set; }

        public DateTime JoinedWeb { get; set; }
    }
}
