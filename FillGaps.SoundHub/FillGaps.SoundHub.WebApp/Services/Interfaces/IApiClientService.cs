using FillGaps.SoundHub.WebApp.Models.Account;

namespace FillGaps.SoundHub.WebApp.Services.Interfaces
{
    public interface IApiClientService
    {
        Task<bool> LoginAsync(LoginViewModel viewModel);
        Task<bool> RegisterAsync(RegisterViewModel viewModel);
        // ...
    }
}
