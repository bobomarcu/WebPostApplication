using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PostApplication.Data;
using PostApplication.Models;

namespace PostApplication.Pages.Packages
{
    public class IndexModel : PageModel
    {
        private readonly PostApplication.Data.PostApplicationContext _context;

        public IndexModel(PostApplication.Data.PostApplicationContext context)
        {
            _context = context;
        }

        public IList<Package> Package { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Package != null)
            {
                Package = await _context.Package
                .Include(p => p.AssignedCourier)
                .Include(p => p.Receiver)
                .Include(p => p.Sender).ToListAsync();
            }
        }
    }
}
