using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CoreEFTest.DataContext
{
    public class CoreStudentsContext : DbContext
    {
        public CoreStudentsContext(DbContextOptions<CoreStudentsContext> options)
        : base(options)
        {
        }
        public virtual DbSet<CoreEFTest.EFModels.Group> Group { get; set; }
        public virtual DbSet<CoreEFTest.EFModels.Student> Student { get; set; }
    }
}