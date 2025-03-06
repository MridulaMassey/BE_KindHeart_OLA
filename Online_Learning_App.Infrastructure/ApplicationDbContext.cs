
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Online_Learning_App.Domain.Entities;

namespace Online_Learning_App.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
