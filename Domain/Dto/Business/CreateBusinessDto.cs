using Domain.Common;
using Domain.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ICollection<ScheduleDto> schedules { get; set; }    
        public IFormFile? BusinessPic { get; set; }
        public IFormFile? LicencePic { get; set; }


        public CreateBusinessDto()
        {
            this.schedules = new List<ScheduleDto>();
        }
    }
    [ModelBinder(BinderType = typeof(CustomModelBinder))]
    public class ScheduleDto
    {
        public string DayOfWeek { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public Guid BusinessId { get; set; }
    }
}
