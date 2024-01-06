using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PostApplication.Data;
using PostApplication.Models;

namespace PostApplication.Pages.Couries
{
    public class DetailsModel : PageModel
    {
        private readonly PostApplication.Data.PostApplicationContext _context;

        public DetailsModel(PostApplication.Data.PostApplicationContext context)
        {
            _context = context;
        }

        public Courier Courier { get; set; } = default!;
        public List<Package> AssignedPackages { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Courier == null)
            {
                return NotFound();
            }
            
            var courier = await _context.Courier.FirstOrDefaultAsync(m => m.Id == id);
            if (courier == null)
            {
                return NotFound();
            }
            else 
            {
                Courier = courier;
                AssignedPackages = _context.Package
                .Where(p => p.AssignedCourierId == id)
                .Include(p => p.Sender)
                .Include(p => p.Receiver)
                .ToList();

            }
            return Page();
        }
    }
}
