using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.Client
{
    public class GetBookingDto
    {
        public Guid Id { get; set; }   
        public string BusinessName { get; set; }
        public DateTime BookDate { get; set; }
        public string AppoientmentState { get; set; } 

    }
}
