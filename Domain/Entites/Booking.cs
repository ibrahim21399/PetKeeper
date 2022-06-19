using Domain.Entites.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Booking:BaseEntity
    {
        public DateTime BookDate { get; set; }  
        public Guid ApplicationUserId { get; set; }
        public Guid BusinessId { get; set; }
        public Guid ScheduleId { get; set; }
        public bool IsCanceled { get; set; }  
        public bool status { get; set; }

        public Schedule Schedule { get; set; }  
    }
}
