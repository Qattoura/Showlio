using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Showlio.api.Models;

namespace Showlio.api.Data
{
    public class ApplicationDBContext : DbContext 
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) 
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

    }
}
