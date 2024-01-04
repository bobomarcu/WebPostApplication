using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PostApplication.Data;
using PostApplication.Models;

namespace PostApplication.Pages.Packages
{
    public class EditModel : PageModel
    {
        private readonly PostApplication.Data.PostApplicationContext _context;

        public EditModel(PostApplication.Data.PostApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Package Package { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Package == null)
            {
                return NotFound();
            }

            var package =  await _context.Package.FirstOrDefaultAsync(m => m.ID == id);
            if (package == null)
            {
                return NotFound();
            }
            Package = package;
           ViewData["AssignedCourierId"] = new SelectList(_context.Courier, "Id", "Id");
           ViewData["ReceiverId"] = new SelectList(_context.User, "Id", "Id");
           ViewData["SenderId"] = new SelectList(_context.User, "Id", "Id");
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

            _context.Attach(Package).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageExists(Package.ID))
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

        private bool PackageExists(int id)
        {
          return (_context.Package?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
