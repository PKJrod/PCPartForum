using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCPartForum.Data;
using PCPartForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCPartForum.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // INDEX/INFO
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Electronic> electronics = await ElectronicsDb.GetElectronicsAsync(_context);

            return View(electronics);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Electronic elect = await ElectronicsDb.GetElectronicAsync(_context, id);
            return View(elect);
        }

        // ADD
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Electronic elect)
        {
            if(ModelState.IsValid)
            {
                await ElectronicsDb.AddElectronicAsync(_context, elect);

                return RedirectToAction("Index");
            }

            return View();
        }


        // EDIT
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Electronic elect = await ElectronicsDb.GetElectronicAsync(_context, id);
            return View(elect);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Electronic elect)
        {
            if(ModelState.IsValid)
            {
                _context.Entry(elect).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // DELETE
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Electronic elect = await ElectronicsDb.GetElectronicAsync(_context, id);

            return View(elect);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteElectronic(int id)
        {
            Electronic elect = await ElectronicsDb.GetElectronicAsync(_context, id);

            _context.Entry(elect).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> SearchResults(string search)
        {
            List<Electronic> electronics = await ElectronicsDb.GetSearchElectronicsAsync(_context, search);

            return View(electronics);
        }
    }
}
