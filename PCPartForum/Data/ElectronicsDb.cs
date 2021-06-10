using Microsoft.EntityFrameworkCore;
using PCPartForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCPartForum.Data
{
    public class ElectronicsDb
    {
        public async static Task<List<Electronic>> GetElectronicsAsync(ApplicationDbContext _context)
        {
            return await (from electronic in _context.Electronics
                          orderby electronic.Name ascending
                          select electronic).ToListAsync();
        }

        public async static Task<List<Electronic>> GetRecentElectronicsAsync(ApplicationDbContext _context)
        {
            return await (from electronic in _context.Electronics
                          orderby electronic.TimeCreated descending
                          select electronic).ToListAsync();
        }

        public static async Task<Electronic> AddElectronicAsync(ApplicationDbContext _context, Electronic electronic)
        {
            _context.Electronics.Add(electronic);
            await _context.SaveChangesAsync();
            return electronic;
        }

        public static async Task<Electronic> GetElectronicAsync(ApplicationDbContext _context, int ProductId)
        {
            Electronic electronic = await (from elect in _context.Electronics
                                           where elect.ProductId == ProductId
                                           select elect).SingleAsync();
            return electronic;
        }

        public async static Task<List<Electronic>> GetSearchElectronicsAsync(ApplicationDbContext _context, string search)
        {
            return await (from electronic in _context.Electronics
                          where electronic.Name.Contains(search)
                          orderby electronic.Name ascending
                          select electronic).ToListAsync();
        }
    }
}
