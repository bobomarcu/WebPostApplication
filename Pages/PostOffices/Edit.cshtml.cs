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

namespace PostApplication.Pages.PostOffices
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
        public PostOffice PostOffice { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PostOffice == null)
            {
                return NotFound();
            }

            var postoffice =  await _context.PostOffice.FirstOrDefaultAsync(m => m.Id == id);
            if (postoffice == null)
            {
                return NotFound();
            }
            PostOffice = postoffice;
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

            _context.Attach(PostOffice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostOfficeExists(PostOffice.Id))
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

        private bool PostOfficeExists(int id)
        {
          return (_context.PostOffice?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
