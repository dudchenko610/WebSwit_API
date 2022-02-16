using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using WebSwIT.Entities.Entities;

namespace WebSwIT.DataAccessLayer.AppContext
{
    public class ApplicationContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}
        
        public DbSet<RoleEmployee> RoleEmployees { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeInRoleEmployee> EmployeeInRoleEmployees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<WorkSample> WorkSamples { get; set; }
        public DbSet<WorkSamplePicture> WorkSamplePictures { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Proposal> Proposals { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<EmployeeInRoleEmployee>()
                .HasKey(employyeInRole => new
                {
                    employyeInRole.EmployeeId,
                    employyeInRole.RoleEmployeeId
                });

            builder.Entity<EmployeeInRoleEmployee>(employeeInRole =>
            {
                employeeInRole.HasOne(employeeInRole => employeeInRole.Employee)
                              .WithMany(employeeInRole => employeeInRole.EmployeeInRoleEmployees)
                              .HasForeignKey(employeeInRole => employeeInRole.EmployeeId);

                employeeInRole.HasOne(employeeInRole => employeeInRole.RoleEmployee)
                              .WithMany(employeeInRole => employeeInRole.EmployeeInRoleEmployee)
                              .HasForeignKey(employeeInRole => employeeInRole.RoleEmployeeId);
            });

            builder.Entity<Contact>()
                .HasOne(c => c.User1)
                .WithMany(us => us.Contacts1)
                .HasForeignKey(us => us.UserId1)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Contact>()
                .HasOne(c => c.User2)
                .WithMany(us => us.Contacts2)
                .HasForeignKey(us => us.UserId2)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
