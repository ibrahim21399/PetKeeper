using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.General.Auth
{
    public class ForgetPasswordDto
    {
        [Required(ErrorMessage = "Email Filed is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address ex:example@gmail.com")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Url Filed is Required")]
        public string Url { get; set; }
    }
}
