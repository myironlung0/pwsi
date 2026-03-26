using CoreEFTest.DataContext;
using CoreEFTest.EFModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lab1
{
    public class IndexModel : PageModel
    {
        private readonly CoreStudentsContext _context;

        public IndexModel(CoreStudentsContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get; set; }
        public IList<Group> Group { get; set; }

        public void OnGet()
        {
            Student = _context.Student
                .Include(s => s.Group)
                .ToList();
            Group = _context.Group
                .ToList();
        }
    }
}