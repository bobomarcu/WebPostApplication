using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PostApplication.Data;
using PostApplication.Models;

namespace PostApplication.Pages.PostOffices
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly PostApplication.Data.PostApplicationContext _context;

        public DeleteModel(PostApplication.Data.PostApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
      public PostOffice PostOffice { get; set; } = default!;

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
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.PostOffice == null)
            {
                return NotFound();
            }
            var postoffice = await _context.PostOffice.FindAsync(id);

            if (postoffice != null)
            {
                PostOffice = postoffice;
                _context.PostOffice.Remove(PostOffice);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
