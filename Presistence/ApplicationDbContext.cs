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


    }

}
