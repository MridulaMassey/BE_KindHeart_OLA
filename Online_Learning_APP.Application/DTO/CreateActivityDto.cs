using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_APP.Application.DTO
{
    public class CreateActivityDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PdfUrl { get; set; }
        public DateTime DueDate { get; set; }
        public string ClassLevel { get; set; }
        public Guid TeacherId { get; set; }
        public Guid? ClassGroupId { get; set; }
        public string ActivityName { get; set; }
    }
}
