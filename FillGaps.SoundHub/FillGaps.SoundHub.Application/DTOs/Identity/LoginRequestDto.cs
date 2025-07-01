using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Application.DTOs.Identity
{
    public class LoginRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
