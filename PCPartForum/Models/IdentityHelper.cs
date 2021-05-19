using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PCPartForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCPartForum.Models
{
    public static class IdentityHelper
    {
        // Role names
        public const string Contributor = "Contributor";
        public const string TrustedSource = "TrustedSource";
        public const string Informant = "Informant";
        public const string Admin = "Admin";


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

        public static async Task<String> FindByUsernameOrEmailAsync
            (UserManager<UserProfile> userManager, string usernameOrEmail)
        {
            if (usernameOrEmail.Contains("@"))
            {
                var user = await userManager.FindByEmailAsync(usernameOrEmail);
                if (user != null)
                {
                    usernameOrEmail = user.UserName;
                }
            }

            return usernameOrEmail;
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-5.0
        /// https://stackoverflow.com/questions/42471866/how-to-create-roles-in-asp-net-core-and-assign-them-to-users
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="roles">params is use to pass in an array or pass in a comma seperated list of values. passes in the list of roles for login</param>
        /// <returns></returns>
        public static async Task CreateRoles(IServiceProvider provider, params string[] roles)
        {
            RoleManager<IdentityRole> roleManager =
                provider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach(string role in roles)
            {
                bool doesRoleExist = await roleManager.RoleExistsAsync(role);
                if(!doesRoleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        /// <summary>
        /// Will give a role to designated email or name
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        internal static async Task CreateDefaultAdmin(IServiceProvider serviceProvider)
        {
            const string email = "ForumManager@cptc.edu";
            const string username = "ForumAdmin";
            const string password = "Gettingstarted";


            var userManager = serviceProvider.GetRequiredService<UserManager<UserProfile>>();

            // Check if any users are in database
            if (userManager.Users.Count() == 0)
            {
                UserProfile admin = new UserProfile()

                {
                    Email = email,
                    UserName = username
                };

                // Create instructor
                await userManager.CreateAsync(admin, password);

                // Add to instructor role
                await userManager.AddToRoleAsync(admin, Admin);
            }
        }
    }
}
