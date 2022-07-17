using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.Client
{
    public class BookDto
    {
        public DateTime bookDate { get; set; }
        public Guid busId { get; set; }
        public Guid scheduleId { get; set; }

    }
}
