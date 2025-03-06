using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Learning_App.Domain.Models
{
    using System.ComponentModel.DataAnnotations;

    namespace KindHeartApi.Models
    {
        public class Login
        {
            [Key]
            public int Id { get; set; }

            [Required]
            public string Username { get; set; } = string.Empty; // Default value to avoid nullable warning

            [Required]
            public string PasswordHash { get; set; } = string.Empty; // Default value

            //	[Required]
            //public string Email { get; set; } = string.Empty; // Default value

            [Required]
            public string Role { get; set; } = "Student"; // Ensure Role is included
        }
    }

}
