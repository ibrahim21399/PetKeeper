using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites.General
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Created_Date { get; set; } = DateTime.Now;
    }
}
