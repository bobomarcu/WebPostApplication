using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Include ILogger
using PostApplication.Data;
using PostApplication.Models;

namespace PostApplication.Pages.Packages
{
    public class EditModel : PageModel
    {
        private readonly PostApplication.Data.PostApplicationContext _context;
        private readonly ILogger<EditModel> _logger; // Include ILogger

        public EditModel(PostApplication.Data.PostApplicationContext context, ILogger<EditModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Package Package { get; set; } = default!;

        public SelectList SenderList { get; set; }
        public SelectList ReceiverList { get; set; }
        public SelectList AssignedCourierList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Package == null)
            {
                return NotFound();
            }

            var package = await _context.Package.FirstOrDefaultAsync(m => m.ID == id);
            if (package == null)
            {
                return NotFound();
            }
            Package = package;
            PopulateDropdowns(); 
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

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

        private void PopulateDropdowns()
        {
            var users = _context.User.ToList();
            var couriers = _context.Courier.ToList();

            SenderList = new SelectList(users, "Id", "FullName", Package.SenderId);
            ReceiverList = new SelectList(users, "Id", "FullName", Package.ReceiverId);
            AssignedCourierList = new SelectList(couriers, "Id", "FullName", Package.AssignedCourierId);

            ViewData["SenderList"] = SenderList;
            ViewData["ReceiverList"] = ReceiverList;
            ViewData["AssignedCourierList"] = AssignedCourierList;
        }
    }
}
