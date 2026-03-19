using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CoreEFTest.DataContext;
using CoreEFTest.EFModels;

namespace Lab1.Pages.Gr
{
    public class DetailsModel : PageModel
    {
        private readonly CoreEFTest.DataContext.CoreStudentsContext _context;

        public DetailsModel(CoreEFTest.DataContext.CoreStudentsContext context)
        {
            _context = context;
        }

        public Group Group { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = await _context.Group.FirstOrDefaultAsync(m => m.GroupId == id);
            if (group == null)
            {
                return NotFound();
            }
            else
            {
                Group = group;
            }
            return Page();
        }
    }
}
