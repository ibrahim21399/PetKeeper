using Domain.Entites;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.Business
{
    public class CreateBusinessDto
    {
        public Guid? Id { get; set; }
        public string BusinessName { get; set; }
        public string BusinessDesc { get; set; }
        public string BusinussPhone { get; set; }
        public int CityId { get; set; }
        public int AreaId { get; set; }
        public Guid? ApplicationUserId { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Guid> ServiceId { get; set; }
        public ICollection<Schedule> schedules { get; set; }    
        public IFormFile? BusinessPic { get; set; }
        public IFormFile? LicencePic { get; set; }
    }

}
