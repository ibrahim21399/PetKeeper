using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entites.General
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string FullName { get; set; }
        //Client status ==> True
        //Owner Status ==> False
        public bool? Status { get; set; }    

        public virtual ICollection<Business> Businesses { get; set; } = new List<Business>();
        public ICollection<Booking> Bookings { get; set; }

        //public virtual ICollection<ApplicationRole> AspNetUserRoles { get; set; } = new List<ApplicationRole>();


    }
}
