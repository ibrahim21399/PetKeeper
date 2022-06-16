using Domain.Entites.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Schedule:BaseEntity
    {
      public string DayOfWeek { get; set; }
      public string StartTime { get; set; }
      public string EndTime { get; set; }
    
      public Guid BusinessId { get; set; }



    }
}
