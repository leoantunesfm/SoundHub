using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Domain.UserEngagement.Repositories
{
    public interface IPlaylistRepository
    {
        Task AdicionarAsync(Playlist playlist);
        void Atualizar(Playlist playlist);
        void Remover(Playlist playlist);
        Task<Playlist?> ObterPorIdComMusicasAsync(Guid id);
        Task<Playlist?> ObterPorNomeAsync(string nome, Guid usuarioId); // <-- Adicionado
        Task<IEnumerable<Playlist>> ObterTodasPorUsuarioIdAsync(Guid usuarioId);
        Task<IEnumerable<Playlist>> PesquisarPorTermoAsync(string termo, Guid usuarioId);
    }
}
