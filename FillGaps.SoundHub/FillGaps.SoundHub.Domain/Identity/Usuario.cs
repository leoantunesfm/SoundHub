using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FillGaps.SoundHub.Domain.Billing;
using FillGaps.SoundHub.Domain.Catalog;
using FillGaps.SoundHub.Domain.UserEngagement;
using Microsoft.AspNetCore.Identity;

namespace FillGaps.SoundHub.Domain.Identity;
public class Usuario : IdentityUser<Guid>
{
    public string NomeCompleto { get; set; }
    public virtual Assinatura Assinatura { get; set; }
    public virtual ICollection<Playlist> Playlists { get; private set; } = new List<Playlist>();
    public virtual ICollection<Musica> MusicasFavoritas { get; private set; } = new List<Musica>();
    public virtual ICollection<Artista> ArtistasFavoritos { get; private set; } = new List<Artista>();
    
    public void FavoritarMusica(Musica musica)
    {
        // Regra de negócio: não adicionar músicas duplicadas.
        if (!MusicasFavoritas.Any(m => m.Id == musica.Id))
        {
            MusicasFavoritas.Add(musica);
        }
    }

    public void DesfavoritarMusica(Musica musica)
    {
        var musicaParaRemover = MusicasFavoritas.FirstOrDefault(m => m.Id == musica.Id);
        if (musicaParaRemover != null)
        {
            MusicasFavoritas.Remove(musicaParaRemover);
        }
    }

    public void FavoritarArtista(Artista artista)
    {
        // Regra de negócio: não adicionar artistas duplicados.
        if (!ArtistasFavoritos.Any(a => a.Id == artista.Id))
        {
            ArtistasFavoritos.Add(artista);
        }
    }

    public void DesfavoritarArtista(Artista artista)
    {
        var artistaParaRemover = ArtistasFavoritos.FirstOrDefault(a => a.Id == artista.Id);
        if (artistaParaRemover != null)
        {
            ArtistasFavoritos.Remove(artistaParaRemover);
        }
    }

    // Método de consulta útil para a camada de apresentação (UI)
    public bool IsMusicaFavorita(Guid musicaId)
    {
        return MusicasFavoritas.Any(m => m.Id == musicaId);
    }

    public bool IsArtistaFavorito(Guid artistaId)
    {
        return ArtistasFavoritos.Any(a => a.Id == artistaId);
    }
}
