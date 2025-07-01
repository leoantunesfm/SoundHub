using FillGaps.SoundHub.WebApp.Models.Account;
using FillGaps.SoundHub.WebApp.Models.Billing;
using FillGaps.SoundHub.WebApp.Models.Catalog;
using FillGaps.SoundHub.WebApp.Models.UserEngagement;
using FillGaps.SoundHub.WebApp.Services.Interfaces;
using System.Net;
using System.Net.Http.Headers;

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

        public async Task<string?> LoginAsync(LoginViewModel viewModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", viewModel);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseViewModel>();
            return loginResponse?.Token;
        }

        public async Task<bool> RegisterAsync(RegisterViewModel viewModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/registrar", viewModel);
            return response.IsSuccessStatusCode;
        }

        public async Task<AssinaturaViewModel?> ObterMinhaAssinaturaAsync()
        {
            AdicionarJwtAoHeader();
            var response = await _httpClient.GetAsync("api/assinaturas/minha-assinatura");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AssinaturaViewModel>();
        }

        public async Task<IEnumerable<PlanoViewModel>> ObterPlanosAtivosAsync()
        {
            AdicionarJwtAoHeader();
            return await _httpClient.GetFromJsonAsync<IEnumerable<PlanoViewModel>>("api/planos/ativos");
        }

        public async Task<bool> CriarAssinaturaAsync(Guid planoId)
        {
            AdicionarJwtAoHeader();

            var requestBody = new { planoId };

            var response = await _httpClient.PostAsJsonAsync("api/assinaturas", requestBody);
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<ArtistaViewModel>> ObterTodosArtistasAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<ArtistaViewModel>>("api/artistas");
        }

        public async Task<bool> FavoritarArtistaAsync(Guid artistaId)
        {
            AdicionarJwtAoHeader();
            var response = await _httpClient.PostAsync($"api/usuario/favoritos/artistas/{artistaId}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DesfavoritarArtistaAsync(Guid artistaId)
        {
            AdicionarJwtAoHeader();
            var response = await _httpClient.DeleteAsync($"api/usuario/favoritos/artistas/{artistaId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<FavoritosViewModel?> ObterMeusFavoritosAsync()
        {
            AdicionarJwtAoHeader();
            return await _httpClient.GetFromJsonAsync<FavoritosViewModel>("api/usuario/favoritos");
        }

        public async Task<ArtistasIndexViewModel> ObterDadosPaginaArtistasAsync()
        {
            AdicionarJwtAoHeader();

            // Faz as duas chamadas à API em paralelo para mais eficiência
            var todosArtistasTask = _httpClient.GetFromJsonAsync<IEnumerable<ArtistaViewModel>>("api/artistas");
            var meusFavoritosTask = ObterMeusFavoritosAsync();

            await Task.WhenAll(todosArtistasTask, meusFavoritosTask);

            var todosArtistas = await todosArtistasTask;
            var meusFavoritos = await meusFavoritosTask;

            var viewModel = new ArtistasIndexViewModel
            {
                TodosArtistas = todosArtistas ?? new List<ArtistaViewModel>(),
                ArtistasFavoritosIds = meusFavoritos?.ArtistasFavoritos.Select(a => a.Id).ToHashSet() ?? new HashSet<Guid>()
            };

            return viewModel;
        }

        public async Task<MusicasIndexViewModel> ObterDadosPaginaMusicasAsync()
        {
            AdicionarJwtAoHeader();

            var todasMusicasTask = _httpClient.GetFromJsonAsync<IEnumerable<MusicaViewModel>>("api/musicas");
            var meusFavoritosTask = ObterMeusFavoritosAsync();

            await Task.WhenAll(todasMusicasTask, meusFavoritosTask);

            var todasMusicas = await todasMusicasTask;
            var meusFavoritos = await meusFavoritosTask;

            return new MusicasIndexViewModel
            {
                TodasMusicas = todasMusicas,
                MusicasFavoritasIds = meusFavoritos?.MusicasFavoritas.Select(m => m.Id).ToHashSet() ?? new HashSet<Guid>()
            };
        }

        public async Task<bool> FavoritarMusicaAsync(Guid musicaId)
        {
            AdicionarJwtAoHeader();
            var response = await _httpClient.PostAsync($"api/usuario/favoritos/musicas/{musicaId}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DesfavoritarMusicaAsync(Guid musicaId)
        {
            AdicionarJwtAoHeader();
            var response = await _httpClient.DeleteAsync($"api/usuario/favoritos/musicas/{musicaId}");
            return response.IsSuccessStatusCode;
        }

        private void AdicionarJwtAoHeader()
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["jwt"];
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
