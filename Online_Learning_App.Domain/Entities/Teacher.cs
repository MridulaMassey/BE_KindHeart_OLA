using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_App.Domain.Entities
{
    public class Teacher : ApplicationUser
    {
        //public Guid RoleId { get; set; } // Link to Role
        //public Role Role { get; set; }
        public Guid UserId { get; set; } // Foreign key to ApplicationUser
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public ICollection<ClassGroup> ClassGroups { get; set; } = new List<ClassGroup>();
        //public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}
