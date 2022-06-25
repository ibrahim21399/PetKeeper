using Domain.Entites.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Comments:BaseEntity
    {
        public String Comment { get; set; }
        public float Rate { get; set; }
        public Guid BusinessId { get; set; }
        public Guid ApplicationUserId { get; set; }

    }
}
