using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PostApplication.Data;
using PostApplication.Models;

namespace PostApplication.Pages.Packages
{
    public class CreateModel : PageModel
    {
        private readonly PostApplication.Data.PostApplicationContext _context;

        private readonly ILogger<CreateModel> _logger;

        public CreateModel(PostApplication.Data.PostApplicationContext context,ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            PopulateDropdowns();
            return Page();
        }

        [BindProperty]
        public Package Package { get; set; } = default!;

        public SelectList SenderList { get; set; }
        public SelectList ReceiverList { get; set; }
        public SelectList AssignedCourierList { get; set; }

        private void PopulateDropdowns()
        {
            var users = _context.User.ToList();
            var couriers = _context.Courier.ToList();

            SenderList = new SelectList(users, "Id", "FullName");
            ReceiverList = new SelectList(users, "Id", "FullName");
            AssignedCourierList = new SelectList(couriers, "Id", "FullName");

            ViewData["SenderList"] = SenderList;
            ViewData["ReceiverList"] = ReceiverList;
            ViewData["AssignedCourierList"] = AssignedCourierList;
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {

                var sender = await _context.User.FindAsync(Package.SenderId);
                var receiver = await _context.User.FindAsync(Package.ReceiverId);
                var courier = await _context.Courier.FindAsync(Package.AssignedCourierId);

                Package.Sender = sender;
                Package.Receiver = receiver;
                Package.AssignedCourier = courier;

                _context.Package.Add(Package);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(Package.ToString());

                return RedirectToPage("/Error");
            }
        }
        }
    }
