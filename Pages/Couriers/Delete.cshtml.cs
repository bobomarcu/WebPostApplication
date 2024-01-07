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

namespace PostApplication.Pages.Couries
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
      public Courier Courier { get; set; } = default!;

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
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Courier == null)
            {
                return NotFound();
            }
            var courier = await _context.Courier.FindAsync(id);

            if (courier != null)
            {
                Courier = courier;
                _context.Courier.Remove(Courier);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
