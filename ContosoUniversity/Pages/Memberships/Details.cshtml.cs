using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Memberships
{
    public class DetailsModel : PageModel
    {
        private readonly SchoolContext _context;

        public DetailsModel(SchoolContext context)
        {
            _context = context;
        }

        public Membership Membership { get; set; }
        public Student Student1 { get; set; }
        public Student Student2 { get; set; }
        public IList<Course> Courses { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Membership = await _context.Memberships
                .Include(m => m.Student1)
                .Include(m => m.Student2)
                .FirstOrDefaultAsync(m => m.MemberID == id);

            if (Membership == null)
            {
                return NotFound();
            }

            // Load courses associated with the membership
            Courses = await _context.Enrollments
                .Where(e => e.StudentID == Membership.Student1ID || e.StudentID == Membership.Student2ID)
                .Select(e => e.Course)
                .ToListAsync();

            return Page();
        }

        public IActionResult OnPostViewCourse(int courseId)
        {
            return RedirectToPage("/Courses/Details", new { id = courseId });
        }
    }
}
