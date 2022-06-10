using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entites.General
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string FullName { get; set; }

        public virtual ICollection<Business> Businesses { get; set; } = new List<Business>();
        //public virtual ICollection<ApplicationRole> AspNetUserRoles { get; set; } = new List<ApplicationRole>();


    }
}
