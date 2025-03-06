using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_App.Domain.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    namespace KindHeartApi.Models
    {
        public class User
        {
            [Key]
            public int Id { get; set; }

            [Required]
            public string FirstName { get; set; } = string.Empty;

            [Required]
            public string LastName { get; set; } = string.Empty;

            [Required]
            public DateTime Birthday { get; set; }

            public string? Gender { get; set; } // Nullable

            [Required]
            public string Position { get; set; } = "Student";

            public string? Phone { get; set; } // Nullable

            public string? Address { get; set; } // Nullable

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

            [Required]
            public int LoginId { get; set; }
        }
    }

}
