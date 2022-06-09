using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto.General.Auth
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "FullName is Requried")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email is Requried")]
        [EmailAddress(ErrorMessage = "Email dosn't match")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is Requried")]
        
        public string? Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Password Not Match")]
        public string? ConfirmPassword { get; set; }
        //[Required]
        //public string? Role { get; set; }
        //public bool IsActive { get; set; }
        public string?PhoneNumber { get; set; }
    }
}
