using Microsoft.AspNetCore.Identity;


namespace Domain.Entites.General
{
    public class ApplicationRole:IdentityRole<Guid>
    {
        //public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; } = new List<ApplicationUser>();

    }
}
