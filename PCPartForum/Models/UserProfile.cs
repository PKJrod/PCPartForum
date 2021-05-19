using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Key]
        public int UserId { get; set; }

        public string Bio { get; set; }

        [Display(Name = "UserPhoto")]
        public byte[] ProfilePicture { get; set; }

        public DateTime JoinedWeb { get; set; }
    }
}
