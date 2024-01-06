using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PostApplication.Data;
using PostApplication.Models;

namespace PostApplication.Pages.Cashiers
{
    public class DeleteModel : PageModel
    {
        private readonly PostApplication.Data.PostApplicationContext _context;

        public DeleteModel(PostApplication.Data.PostApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Cashier Cashier { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Cashier == null)
            {
                return NotFound();
            }

            var cashier = await _context.Cashier.FirstOrDefaultAsync(m => m.Id == id);

            if (cashier == null)
            {
                return NotFound();
            }
            else 
            {
                Cashier = cashier;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Cashier == null)
            {
                return NotFound();
            }
            var cashier = await _context.Cashier.FindAsync(id);

            if (cashier != null)
            {
                Cashier = cashier;
                _context.Cashier.Remove(Cashier);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
