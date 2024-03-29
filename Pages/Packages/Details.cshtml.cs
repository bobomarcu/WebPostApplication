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

namespace PostApplication.Pages.Packages
{
    [Authorize(Roles = "Admin,Cashier,Courier")]
    public class DetailsModel : PageModel
    {
        private readonly PostApplication.Data.PostApplicationContext _context;

        public DetailsModel(PostApplication.Data.PostApplicationContext context)
        {
            _context = context;
        }

      public Package Package { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Package == null)
            {
                return NotFound();
            }

            var package = await _context.Package.Include(p => p.Sender).Include(p => p.Receiver).Include(p => p.AssignedCourier).FirstOrDefaultAsync(m => m.ID == id);
            if (package == null)
            {
                return NotFound();
            }
            else 
            {
                Package = package;
            }
            return Page();
        }
    }
}
