using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Contracts.DTOs
{
    public class AuthenticationDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        //public string CaptchaToken { get; set; }
    }
}
