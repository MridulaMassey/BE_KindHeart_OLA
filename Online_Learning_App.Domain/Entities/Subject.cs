using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_App.Domain.Entities
{
    public class Subject
    {
        public Guid SubjectId { get; set; }
        public string SubjectName { get; set; } // Example: "Physics", "Mathematics"

        // Many-to-Many Relationship with ClassGroup
        public ICollection<ClassGroupSubject> ClassGroups { get; set; } = new List<ClassGroupSubject>();
    }
}
