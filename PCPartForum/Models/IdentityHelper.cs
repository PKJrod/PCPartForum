using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PCPartForum.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        public enum FileType
        {
            Photo, Video, Audio, Text
        }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool IsProfilePictureNotSet(IFormFile file)
        {
            if(file == null)
            {
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsValidExtension(IFormFile file, FileType type)
        {
            string extension = Path.GetExtension(file.FileName).ToLower();

            switch(type)
            {
                case FileType.Photo:
                    string[] userPictureExtension = { ".png", ".gif", ".jpg" };
                    if (userPictureExtension.Contains(extension))
                        return true;
                    return false;
                case FileType.Video:
                    break;
                case FileType.Audio:
                    break;
                case FileType.Text:
                    break;
                default:
                    break;
            }

            return false;
        }

        /// <summary>
        /// 
        /// https://stackoverflow.com/questions/37628743/instantiating-an-iformfile-from-a-physical-file
        /// </summary>
        /// <param name="physicalFile"></param>
        /// <returns></returns>
        public static IFormFile ConvertingDefaultProfiletoFormFile(FileInfo physicalFile)
        {

            var fileMock = new Mock<IFormFile>();
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            using (FileStream fs = physicalFile.OpenRead())
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);

                while (fs.Read(b, 0, b.Length) > 0)
                {
                    writer.WriteLine(temp.GetString(b));
                }
            }
            writer.Flush();
            ms.Position = 0;
            var fileName = physicalFile.Name;
            //Setup mock file using info from physical file
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            fileMock.Setup(m => m.OpenReadStream()).Returns(ms);
            fileMock.Setup(m => m.ContentDisposition).Returns(string.Format("inline; filename={0}", fileName));
            //...setup other members as needed

            return fileMock.Object;
        }
    }
}
