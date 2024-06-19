using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Pages.Memberships
{
    public class IndexModel : PageModel
    {
        private readonly SchoolContext _context;

        public IndexModel(SchoolContext context)
        {
            _context = context;
        }

        public IList<Membership> Memberships { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Memberships = await _context.Memberships
                .Include(m => m.Student1)
                .Include(m => m.Student2)
                .AsNoTracking()
                .ToListAsync();

            return Page();
        }
    }
}