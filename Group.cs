using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace CoreEFTest.EFModels
{
    [Table("Group")]
    public class Group
    {
        public Group()
        {
            Students = new HashSet<Student>();
        }
        [Key]
        public int GroupId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}