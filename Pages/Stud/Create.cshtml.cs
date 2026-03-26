using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CoreEFTest.DataContext;
using CoreEFTest.EFModels;

namespace Lab1.Pages.Stud
{
    public class CreateModel : PageModel
    {
        private readonly CoreEFTest.DataContext.CoreStudentsContext _context;

        public CreateModel(CoreEFTest.DataContext.CoreStudentsContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["GroupId"] = new SelectList(_context.Group, "GroupId", "Name");
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Student.Add(Student);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}