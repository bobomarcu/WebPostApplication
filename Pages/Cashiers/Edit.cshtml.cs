using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PostApplication.Data;
using PostApplication.Models;

namespace PostApplication.Pages.Cashiers
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly PostApplication.Data.PostApplicationContext _context;

        public EditModel(PostApplication.Data.PostApplicationContext context)
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

            var cashier =  await _context.Cashier.FirstOrDefaultAsync(m => m.Id == id);
            if (cashier == null)
            {
                return NotFound();
            }
            Cashier = cashier;
           ViewData["PostOfficeID"] = new SelectList(_context.PostOffice, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Cashier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CashierExists(Cashier.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CashierExists(int id)
        {
          return (_context.Cashier?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
