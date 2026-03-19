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
    public class IndexModel : PageModel
    {
        private readonly CoreEFTest.DataContext.CoreStudentsContext _context;

        public IndexModel(CoreEFTest.DataContext.CoreStudentsContext context)
        {
            _context = context;
        }

        public IList<Group> Group { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Group = await _context.Group.ToListAsync();
        }
    }
}
