using Domain.Entites;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.Business
{
    public class GetBusinessDto 
    {
        public Guid Id { get; set; }
        public string BusinessName { get; set; }
        public string BusinessDesc { get; set; }
        public string BusinussPhone { get; set; }
        public string MangerName { get; set; }    
        public string CityName { get; set; }
        public string AreaName { get; set; }
        public float BusinessRate { get; set; }

        public ICollection<string> Services{ get; set; }
        public string BusinessPic { get; set; }
    }
}
