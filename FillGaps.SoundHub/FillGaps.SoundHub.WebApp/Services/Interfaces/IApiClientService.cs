using FillGaps.SoundHub.WebApp.Models.Account;
using FillGaps.SoundHub.WebApp.Models.Billing;
using FillGaps.SoundHub.WebApp.Models.Catalog;
using FillGaps.SoundHub.WebApp.Models.UserEngagement;

namespace FillGaps.SoundHub.WebApp.Services.Interfaces
{
    public interface IApiClientService
    {
        Task<string?> LoginAsync(LoginViewModel viewModel);
        Task<(bool Success, string? ErrorMessage)> RegisterAsync(RegisterViewModel viewModel);
        Task<AssinaturaViewModel?> ObterMinhaAssinaturaAsync();
        Task<IEnumerable<PlanoViewModel>> ObterPlanosAtivosAsync();
        Task<bool> CriarAssinaturaAsync(Guid planoId);
        Task<IEnumerable<ArtistaViewModel>> ObterTodosArtistasAsync();
        Task<bool> FavoritarArtistaAsync(Guid artistaId);
        Task<bool> DesfavoritarArtistaAsync(Guid artistaId);
        Task<FavoritosViewModel?> ObterMeusFavoritosAsync();
        Task<ArtistasIndexViewModel> ObterDadosPaginaArtistasAsync(string? termoBusca);
        Task<MusicasIndexViewModel> ObterDadosPaginaMusicasAsync(string? termoBusca);
        Task<bool> FavoritarMusicaAsync(Guid musicaId);
        Task<bool> DesfavoritarMusicaAsync(Guid musicaId);
        Task<IEnumerable<ArtistaViewModel>> PesquisarArtistasAsync(string termo);
        Task<IEnumerable<MusicaViewModel>> PesquisarMusicasAsync(string termo);
    }
}
