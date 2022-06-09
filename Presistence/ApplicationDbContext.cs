using Domain.Entites.General;
using Domain.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace Presistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<City> cities { get; set; } 
        public DbSet<Area> Areas {get; set; } 
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Service>  Services { get; set; }
        public DbSet<BusinessService> BusinessServices { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BusinessService>().HasKey(a => new { a.ServiceId, a.BusinessId });
            base.OnModelCreating(builder);
        }


    }

}
