using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Memberships
{
    public class CreateModel : PageModel
    {
        private readonly SchoolContext _context;

        public CreateModel(SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Membership Membership { get; set; }

        public IList<Student> Students { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Students = await _context.Students.ToListAsync();
            Membership = new Membership
            {
                SubscriptionStartDate = DateTime.Today,
                SubscriptionEndDate = DateTime.Today
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Students = await _context.Students.ToListAsync();
                return Page();
            }

            // Ensure Student1ID and Student2ID are different
            if (Membership.Student1ID == Membership.Student2ID)
            {
                ModelState.AddModelError(string.Empty, "Student 1 and Student 2 must be different.");
                Students = await _context.Students.ToListAsync();
                return Page();
            }

            // Check if the combination of Student1ID and Student2ID already exists
            if (await _context.Memberships.AnyAsync(m =>
                (m.Student1ID == Membership.Student1ID && m.Student2ID == Membership.Student2ID) ||
                (m.Student1ID == Membership.Student2ID && m.Student2ID == Membership.Student1ID)))
            {
                ModelState.AddModelError(string.Empty, "This membership already exists.");
                Students = await _context.Students.ToListAsync();
                return Page();
            }

            // Ensure the SubscriptionStartDate is properly set
            if (Membership.SubscriptionStartDate == default(DateTime))
            {
                Membership.SubscriptionStartDate = DateTime.Now;
            }

            _context.Memberships.Add(Membership);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
