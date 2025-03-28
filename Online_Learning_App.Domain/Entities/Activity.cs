using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_App.Domain.Entities
{
    // Domain Entity: Activity
    public class Activity
    {
       
        public Guid ActivityId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PdfUrl { get; set; }
        public DateTime DueDate { get; set; }
        public string ClassLevel { get; set; }
        public double WeightagePercent { get; set; } // Weightage per activity 90
        // Define the foreign key for Teacher to Activity
        public Guid TeacherId { get; set; }
        public Boolean HasFeedback { get; set; } = false;
        public string Feedback { get; set; }

        public Teacher Teacher { get; set; }
        //public Guid ClassGroupId { get; set; }
        //public ClassGroup? ClassGroup { get; set; }
        //// Create the relationship with Submission
        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();

        public Guid Id { get; set; }
        public string ActivityName { get; set; }

        // Make sure this is nullable
        public Guid? ClassGroupId { get; set; }

        public ClassGroup ClassGroup { get; set; }

        [ForeignKey("Subject")]
        public Guid SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
        public ICollection<ActivityGrade> ActivityGrades { get; set; } = new List<ActivityGrade>();


    }
}
