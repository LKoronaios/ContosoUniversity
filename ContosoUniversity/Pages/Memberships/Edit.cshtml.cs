using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Memberships
{
    public class EditModel : PageModel
    {
        private readonly SchoolContext _context;

        public EditModel(SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Membership Membership { get; set; }

        public SelectList StudentList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Membership = await _context.Memberships.FindAsync(id);

            if (Membership == null)
            {
                return NotFound();
            }

            // Populate dropdown list of students
            StudentList = new SelectList(await _context.Students.ToListAsync(), "ID", "FullName");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Reload the student list in case of model validation errors
                await LoadStudentListAsync();
                return Page();
            }

            // Ensure Student1ID and Student2ID are different
            if (Membership.Student1ID == Membership.Student2ID)
            {
                ModelState.AddModelError(string.Empty, "Student 1 and Student 2 must be different.");
                await LoadStudentListAsync();
                return Page();
            }

            // Check if the combination of Student1ID and Student2ID already exists
            if (await MembershipExistsAsync(Membership.Student1ID, Membership.Student2ID, Membership.MemberID))
            {
                ModelState.AddModelError(string.Empty, "This membership already exists.");
                await LoadStudentListAsync();
                return Page();
            }

            // Ensure the SubscriptionStartDate is properly set
            if (Membership.SubscriptionStartDate == default(DateTime))
            {
                Membership.SubscriptionStartDate = DateTime.Now;
            }

            _context.Attach(Membership).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MembershipExists(Membership.MemberID))
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

        private bool MembershipExists(int id)
        {
            return _context.Memberships.Any(e => e.MemberID == id);
        }

        private async Task LoadStudentListAsync()
        {
            StudentList = new SelectList(await _context.Students.ToListAsync(), "ID", "FullName");
        }

        private async Task<bool> MembershipExistsAsync(int student1Id, int student2Id, int membershipId)
        {
            return await _context.Memberships.AnyAsync(m =>
                m.MemberID != membershipId &&
                ((m.Student1ID == student1Id && m.Student2ID == student2Id) ||
                 (m.Student1ID == student2Id && m.Student2ID == student1Id)));
        }
    }
}
