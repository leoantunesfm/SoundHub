using FillGaps.SoundHub.Application.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Application.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task RegisterAsync(RegisterRequestDto dto);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto dto);
    }
}
