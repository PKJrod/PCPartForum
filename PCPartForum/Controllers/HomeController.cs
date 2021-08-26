using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Playwright;
using PCPartForum.Data;
using PCPartForum.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PCPartForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Electronic> electronics = await ElectronicsDb.GetRecentElectronicsAsync(_context);

            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync();
            var page = await browser.NewPageAsync();
            await page.GotoAsync("https://www.newegg.com/d/best-sellers?cm_sp=Head_Navigation-_-Under_Search_Bar-_-Best+Sellers&icid=623089");
            var element1 = await page.QuerySelectorAsync("#item_cell_9SIA12KE7F2072__0:has(a.item-title)");
            var element2 = await element1.QuerySelectorAsync(".item-title");
            var returned = await element2.InnerTextAsync();
            ViewBag.Message = returned;
            return View(electronics);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
