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
       public float  BusinessRate { get; set; }
       [ForeignKey("City")]
       public int CityId { get; set;}
       [ForeignKey("Area")]
       public int AreaId { get; set; }
       public City City { get; set; }
       public Area Area { get; set; }
       [ForeignKey("ApplicationUser")]
       public Guid DocID { get; set; }
       public ApplicationUser ApplicationUser { get; set; }
       public virtual ICollection<Service> Services { get; set; } = new List<Service>();
       //public ICollection<Schedule> Schedules { get; set; }


    }
}
