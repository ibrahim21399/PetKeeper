using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites.General
{
    public class TokenEntity
    {
        public string? Token { get; set; }
        public DateTime? Expiration { get; set; }
        public ApplicationUser CurrentUser { get; set; }
        public bool IsActive { get; set; }
    }
}
