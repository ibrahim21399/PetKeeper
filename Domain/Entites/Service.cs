using Domain.Entites.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Service:BaseEntity
    {
        public string ServiceTitle { get; set; }
        public virtual ICollection<Business> Businesses { get; set; } = new List<Business>();

    }
}
