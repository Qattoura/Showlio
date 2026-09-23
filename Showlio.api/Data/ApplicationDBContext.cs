using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Showlio.api.Identity;
using Showlio.api.Models;

namespace Showlio.api.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        { }


        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ContactItem> ContactItems { get; set; }
        public DbSet<Template> Templates { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly);


            var userRoleId = new Guid("9bee4b22-b843-4d88-82cd-8f12958d969d");
            var adminRoleId = new Guid("43ee448f-61a0-46eb-8c65-463808b39fd2");

            modelBuilder.Entity<IdentityRole<Guid>>().HasData(
                new IdentityRole<Guid>
                {
                    Id = userRoleId,
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "95b12203-fdf1-49c2-9154-ccc25b3c1a50"
                },
                new IdentityRole<Guid>
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "c770e5fc-f65a-45e2-a378-1e743c8f68f1"
                }
            );


        }


    }
}
