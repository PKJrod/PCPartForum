using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PCPartForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCPartForum.Models
{
    public static class IdentityHelper
    {
        public static void SetIdentityOptions(IdentityOptions options)
        {
            // Setting sign in options
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;

            // Set Password strength
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = false;
        }

        public static async Task<IdentityUser> GetUserByEmailUsernameAsync( UserManager<IdentityUser> _userManager)
        {
            IdentityUser foundUser = await _userManager.Users
                                                .Where(user => user.Email == "Email" || user.UserName == "Username")
                                                .SingleOrDefaultAsync();
            return foundUser;
        }
    }
}
