using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.Admin
{
    public class GetAdminBusinessDetailsDto
    {
        public Guid Id { get; set; }
        public string BusinessName { get; set; }
        public string BusinessDesc { get; set; }
        public string BusinussPhone { get; set; }
        public string MangerName { get; set; }
        public string CityName { get; set; }
        public string AreaName { get; set; }
        public float BusinessRate { get; set; }

        public ICollection<string> Services { get; set; }
        public string BusinessPic { get; set; }
        public string LicencePic { get; set; }
    }
}
