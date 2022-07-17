using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.General
{
    public class FilterDto
    {
        public Guid? ServiceId{get; set;}
        public  int? CityId { get; set; }
        public int? AreaId { get; set; }
    }
}
