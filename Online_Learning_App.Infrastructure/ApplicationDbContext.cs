using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Online_Learning_App.Domain.Entities;
using System;

namespace Online_Learning_App.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<ClassGroup> ClassGroups { get; set; } // **Newly Added**

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Rename Identity Tables
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");

            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");

            // Map Entities to Tables
            modelBuilder.Entity<Teacher>().ToTable("Teachers");
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<ClassGroup>().ToTable("ClassGroups");
            modelBuilder.Entity<Activity>().ToTable("Activities");
            modelBuilder.Entity<Submission>().ToTable("Submissions");

            // Define the relationships
        
            // **Allow NULL for ClassGroupId in Activity**
            modelBuilder.Entity<Activity>()
                .HasOne(a => a.ClassGroup)
                .WithMany(cg => cg.Activities)
                .HasForeignKey(a => a.ClassGroupId)
                .OnDelete(DeleteBehavior.SetNull);

            // **ClassGroup - Teacher Relationship**
            modelBuilder.Entity<ClassGroup>()
                .HasOne(cg => cg.Teacher)
                .WithMany(t => t.ClassGroups)
                .HasForeignKey(cg => cg.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);

            // **Student - ClassGroup Relationship (Allow NULL)**
            modelBuilder.Entity<Student>()
                .HasOne(s => s.ClassGroup)
                .WithMany(cg => cg.Students)
                .HasForeignKey(s => s.ClassGroupId)
                .OnDelete(DeleteBehavior.SetNull);

            // **Student - Role Relationship**
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Role)
                .WithMany()
                .HasForeignKey(s => s.RoleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Submission>()
        .HasOne(s => s.Student) // Define the foreign key relationship
        .WithMany(s => s.Submissions)
        .HasForeignKey(s => s.StudentId)
        .OnDelete(DeleteBehavior.Restrict);
            // **Teacher - Role Relationship**
            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Role)
                .WithMany()
                .HasForeignKey(t => t.RoleId)
                .OnDelete(DeleteBehavior.NoAction);
            //modelBuilder.Entity<Student>()
            //  .HasOne(s => s.User)
            //  .WithOne() // One-to-one relationship
            //  .HasForeignKey<Student>(s => s.UserId)
            //  .OnDelete(DeleteBehavior.Cascade);
            // Student - User Relationship
            modelBuilder.Entity<Student>()
                .HasOne(s => s.User)
                .WithOne(u => u.Student)
                .HasForeignKey<Student>(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Adjust as needed

        }

    }
}
