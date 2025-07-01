using FillGaps.SoundHub.WebApp.Models.Account;
using FillGaps.SoundHub.WebApp.Services.Interfaces;

namespace FillGaps.SoundHub.WebApp.Services
{
    public class ApiClientService : IApiClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiClientService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> LoginAsync(LoginViewModel viewModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", viewModel);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseViewModel>();
            if (string.IsNullOrWhiteSpace(loginResponse?.Token))
            {
                return false;
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddHours(8),
                IsEssential = true
            };
            _httpContextAccessor.HttpContext?.Response.Cookies.Append("jwt", loginResponse.Token, cookieOptions);

            return true;
        }

        public async Task<bool> RegisterAsync(RegisterViewModel viewModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/registrar", viewModel);
            return response.IsSuccessStatusCode;
        }
    }
}
