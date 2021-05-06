using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> Index()
        {
            List<Electronic> electronics = await ElectronicsDb.GetElectronicsAsync(_context);

            return View(electronics);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return View();
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            return null;
        }

        // DELETE
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            return null;
        }
    }
}
