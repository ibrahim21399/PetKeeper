using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entites.General
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public bool IsActive { get; set; }
       


        public virtual ICollection<ApplicationRole> UserRoles { get; set; } = new List<ApplicationRole>();


    }
}
