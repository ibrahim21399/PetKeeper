using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.General.Auth
{
    public class LoginDto
    {
        [Required]
        //[Required(ErrorMessageResourceName = nameof(Resources.General.LoginResources.MissingUserName), ErrorMessageResourceType = typeof(Resources.General.LoginResources))]
        public string Email { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        public string Password { get; set; }
    }
}
