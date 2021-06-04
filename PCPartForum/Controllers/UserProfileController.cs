using Microsoft.AspNetCore.Mvc;
using PCPartForum.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCPartForum.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public async Task<IActionResult> Index()
        {
            
            return View();
        }
    }
}
