using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CoreEFTest.DataContext;
using CoreEFTest.EFModels;

namespace Lab1.Pages.Gr
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
            return Page();
        }

        [BindProperty]
        public Group Group { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Group.Add(Group);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
