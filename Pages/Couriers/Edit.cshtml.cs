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

namespace PostApplication.Pages.Couries
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
        public Courier Courier { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Courier == null)
            {
                return NotFound();
            }

            var courier =  await _context.Courier.FirstOrDefaultAsync(m => m.Id == id);
            if (courier == null)
            {
                return NotFound();
            }
            Courier = courier;
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

            _context.Attach(Courier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourierExists(Courier.Id))
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

        private bool CourierExists(int id)
        {
          return (_context.Courier?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
