using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class City
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public ICollection<Area> Areas { get; set; }   
    }

    public class Area
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        [ForeignKey("City")]
        public int CityId { get; set; } 
    }
}
