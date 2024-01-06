using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PostApplication.Data;
using PostApplication.Models;

namespace PostApplication.Pages.PostOffices
{
    public class DetailsModel : PageModel
    {
        private readonly PostApplication.Data.PostApplicationContext _context;

        public DetailsModel(PostApplication.Data.PostApplicationContext context)
        {
            _context = context;
        }

        public PostOffice PostOffice { get; set; } = default!;
        public List<Package> AssignedPackages { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PostOffice == null)
            {
                return NotFound();
            }

            var postoffice = await _context.PostOffice.FirstOrDefaultAsync(m => m.Id == id);
            if (postoffice == null)
            {
                return NotFound();
            }
            else 
            {
                PostOffice = postoffice;
                AssignedPackages = _context.Package
                .Where(p => p.PostOfficeID == id)
                .Include(p => p.Sender)
                .Include(p => p.Receiver)
                .ToList();
            }
            return Page();
        }
    }
}
