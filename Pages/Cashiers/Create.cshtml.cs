using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PostApplication.Data;
using PostApplication.Models;

namespace PostApplication.Pages.Cashiers
{
    public class CreateModel : PageModel
    {
        private readonly PostApplication.Data.PostApplicationContext _context;

        public CreateModel(PostApplication.Data.PostApplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["PostOfficeID"] = new SelectList(_context.PostOffice, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Cashier Cashier { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Cashier == null || Cashier == null)
            {
                return Page();
            }

            _context.Cashier.Add(Cashier);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
