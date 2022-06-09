using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class BusinessService
    {
        [ForeignKey("Business")]
        public Guid BusinessId { get; set; }
        [ForeignKey("Service")]
        public Guid ServiceId { get; set; }

        public Business Business { get; set; }
        public Service Service { get; set; }
    }
}
