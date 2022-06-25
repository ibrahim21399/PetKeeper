using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.Client
{
    public class CreateCommentDto
    {
        public String Comment { get; set; }
        [Required]
        public float Rate { get; set; }
        public Guid ApplicationUserId { get; set; }
    }
}
