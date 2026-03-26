using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CoreEFTest.DataContext;
using CoreEFTest.EFModels;
using System.Linq.Expressions;

namespace Lab1.Pages.Gr
{
    public class DeleteModel : PageModel
    {
        private readonly CoreEFTest.DataContext.CoreStudentsContext _context;

        public DeleteModel(CoreEFTest.DataContext.CoreStudentsContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try{
                var group = await _context.Group.FindAsync(id);
                if (group != null)
                {
                    Group = group;
                    _context.Group.Remove(Group);
                    await _context.SaveChangesAsync();
                }
            }catch(Exception e) { Console.WriteLine("Błąd"); }
            return RedirectToPage("./Index");
        }
    }
}
