﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PostApplication.Data;
using PostApplication.Models;

namespace PostApplication.Pages.Cashiers
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly PostApplication.Data.PostApplicationContext _context;

        public IndexModel(PostApplication.Data.PostApplicationContext context)
        {
            _context = context;
        }

        public IList<Cashier> Cashier { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Cashier != null)
            {
                Cashier = await _context.Cashier
                .Include(c => c.PostOffice).ToListAsync();
            }
        }
    }
}
