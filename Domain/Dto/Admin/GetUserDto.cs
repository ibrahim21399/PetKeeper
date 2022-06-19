using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.Admin
{
    public class GetUserDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public String Email { get; set; }
        public string PhoneNumber { get; set; }


    }
}
