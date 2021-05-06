using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PCPartForum.Models
{
    public class UserProfile
    {
        [Key]
        public int UserId { get; set; }

        public string Bio { get; set; }

        public string ProfilePicture { get; set; }

        public DateTime JoinedWeb { get; set; }
    }
}
