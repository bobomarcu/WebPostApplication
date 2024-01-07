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

namespace PostApplication.Pages.PostOffices
{
    [Authorize(Roles = "Admin,Cashier,Courier")]
    public class IndexModel : PageModel
    {
        private readonly PostApplication.Data.PostApplicationContext _context;

        public IndexModel(PostApplication.Data.PostApplicationContext context)
        {
            _context = context;
        }

        public IList<PostOffice> PostOffice { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.PostOffice != null)
            {
                PostOffice = await _context.PostOffice.ToListAsync();
            }
        }
    }
}
