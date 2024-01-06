using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PostApplication.Data;
using PostApplication.Models;

namespace PostApplication.Pages.PostOffices
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
            return Page();
        }

        [BindProperty]
        public PostOffice PostOffice { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.PostOffice == null || PostOffice == null)
            {
                return Page();
            }

            _context.PostOffice.Add(PostOffice);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
