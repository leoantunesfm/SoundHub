using FillGaps.SoundHub.Domain.Billing;
using FillGaps.SoundHub.Domain.Catalog;
using FillGaps.SoundHub.Domain.Identity;
using FillGaps.SoundHub.Domain.UserEngagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillGaps.SoundHub.Infrastructure.Data
{
    public class SoundHubDbContext : IdentityDbContext<Usuario, IdentityRole<Guid>, Guid>
    {
        // Contexto: Catalog
        public DbSet<Artista> Artistas { get; set; }
        public DbSet<Album> Albuns { get; set; }
        public DbSet<Musica> Musicas { get; set; }
        public DbSet<Genero> Generos { get; set; }

        // Contexto: UserEngagement
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistMusica> PlaylistMusicas { get; set; }

        // Contexto: Billing
        public DbSet<Plano> Planos { get; set; }
        public DbSet<Assinatura> Assinaturas { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }

        public SoundHubDbContext(DbContextOptions<SoundHubDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(255)");
            }

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<Musica>()
                .OwnsOne(m => m.Duracao, duracao =>
                {
                    duracao.Property(d => d.Segundos).HasColumnName("DuracaoEmSegundos");
                });

            builder.Entity<Usuario>()
                .HasMany(u => u.MusicasFavoritas)
                .WithMany(m => m.UsuariosQueFavoritaram)
                .UsingEntity(j => j.ToTable("UsuarioMusicaFavoritos"));

            builder.Entity<Usuario>()
                .HasMany(u => u.ArtistasFavoritos)
                .WithMany(a => a.UsuariosQueFavoritaram)
                .UsingEntity(j => j.ToTable("UsuarioArtistaFavoritos"));

            builder.Entity<PlaylistMusica>()
                .HasKey(pm => new { pm.PlaylistId, pm.MusicaId });

            builder.Entity<Assinatura>()
                .Property(a => a.Status)
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Entity<Transacao>()
                .Property(t => t.Status)
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Entity<Transacao>().Ignore(x => x.DomainEvents);

        }
    }
}
