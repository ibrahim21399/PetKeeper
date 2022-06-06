using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.General.Auth
{
    public class TokenDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public ApplicationUserDto CurrentUser { get; set; }
        public bool IsActive { get; set; }
    }

    public class ApplicationUserDto
    {
        public string? UserName { get; set;}
        public string? FullName { get; set; }
        public string? ProfilePicture { get; set; }
    }

}
