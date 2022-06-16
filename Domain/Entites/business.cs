using Domain.Entites.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Business:BaseEntity
    { 
       public string BusinessName { get; set; }
       public string BusinessDesc { get; set; }
       public string BusinussPhone { get; set; }
       public float  BusinessRate { get; set; }

       [ForeignKey("City")]
       public int CityId { get; set;}

       [ForeignKey("Area")]
       public int AreaId { get; set; }
       public virtual City City { get; set; }
       public virtual Area Area { get; set; }

        [ForeignKey("ApplicationUser")]
        public Guid ApplicationUserId { get; set; } 

       public bool IsActive { get; set; }
       //public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; } = new List<ApplicationUser>();
       public virtual ICollection<BusinessService> BusinessServices { get; set; } = new List<BusinessService>();

       public ICollection<Schedule> Schedules { get; set; }


    }

    //public class BusinessService
    //{
    //    public Guid BusinessId { get; set; }    
    //    public Guid ServiceId{ get; set; }

    //}

}
