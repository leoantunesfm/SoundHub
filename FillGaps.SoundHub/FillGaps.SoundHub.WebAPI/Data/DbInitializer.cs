using FillGaps.SoundHub.Application.DTOs.Billing;
using FillGaps.SoundHub.Application.DTOs.Catalog;
using FillGaps.SoundHub.Application.Services.Interfaces;
using FillGaps.SoundHub.Domain.Catalog.Repositories;
using FillGaps.SoundHub.Domain.SharedKernel.Repositories;

namespace FillGaps.SoundHub.WebAPI.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var planoService = scope.ServiceProvider.GetRequiredService<IPlanoService>();
            var generoService = scope.ServiceProvider.GetRequiredService<IGeneroService>();
            var artistaService = scope.ServiceProvider.GetRequiredService<IArtistaService>();
            var albumService = scope.ServiceProvider.GetRequiredService<IAlbumService>();
            var musicaService = scope.ServiceProvider.GetRequiredService<IMusicaService>();

            var generoRepository = scope.ServiceProvider.GetRequiredService<IGeneroRepository>();
            var musicaRepository = scope.ServiceProvider.GetRequiredService<IMusicaRepository>();
            var artistaRepository = scope.ServiceProvider.GetRequiredService<IArtistaRepository>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            // --- 1. SEED DE PLANOS ---
            if (!(await planoService.ObterPlanosAtivosAsync()).Any())
            {
                await planoService.CriarPlanoAsync(new CriarPlanoRequestDto { Nome = "Plano Grátis", Descricao = "Acesso com anúncios.", Preco = 0.00m });
                await planoService.CriarPlanoAsync(new CriarPlanoRequestDto { Nome = "Plano Premium", Descricao = "Música sem limites e sem anúncios.", Preco = 19.90m });
                await planoService.CriarPlanoAsync(new CriarPlanoRequestDto { Nome = "Plano Família", Descricao = "Até 6 contas Premium.", Preco = 34.90m });
            }

            // --- 2. SEED DE GÊNEROS ---
            if (!(await generoService.ObterTodosGenerosAsync()).Any())
            {
                await generoService.CriarGeneroAsync(new CriarGeneroRequestDto { Nome = "Rock Brasileiro", Descricao = "O rock feito no Brasil, com suas vertentes e épocas." });
                await generoService.CriarGeneroAsync(new CriarGeneroRequestDto { Nome = "MPB", Descricao = "Música Popular Brasileira, um gênero rico e diversificado." });
                await generoService.CriarGeneroAsync(new CriarGeneroRequestDto { Nome = "Sertanejo", Descricao = "Da música caipira de raiz ao sertanejo universitário." });
                await generoService.CriarGeneroAsync(new CriarGeneroRequestDto { Nome = "Samba", Descricao = "O ritmo que é a alma do Brasil, do partido-alto ao samba-enredo." });
                await generoService.CriarGeneroAsync(new CriarGeneroRequestDto { Nome = "Pop", Descricao = "A música popular que domina as paradas de sucesso." });
                await generoService.CriarGeneroAsync(new CriarGeneroRequestDto { Nome = "Metal", Descricao = "Do heavy metal clássico ao thrash e groove metal." });
                await generoService.CriarGeneroAsync(new CriarGeneroRequestDto { Nome = "Axé", Descricao = "A música contagiante que nasceu na Bahia e conquistou o Brasil." });
                await generoService.CriarGeneroAsync(new CriarGeneroRequestDto { Nome = "Eletrônica", Descricao = "Batidas eletrônicas de DJs e produtores nacionais e internacionais." });
                await generoService.CriarGeneroAsync(new CriarGeneroRequestDto { Nome = "Forró", Descricao = "O ritmo dançante do nordeste brasileiro." });
                await generoService.CriarGeneroAsync(new CriarGeneroRequestDto { Nome = "Funk Carioca", Descricao = "O som que nasceu nas comunidades do Rio e ganhou o mundo." });
            }

            // --- 3. SEED DE ARTISTAS, ÁLBUNS E MÚSICAS ---
            if (!(await artistaService.ObterTodosArtistasAsync()).Any())
            {
                var artistasData = GetArtistasData();
                foreach (var artistaData in artistasData)
                {
                    // Cria o Artista
                    var artistaDto = await artistaService.CriarArtistaAsync(new CriarArtistaRequestDto { Nome = artistaData.Nome, Descricao = artistaData.Descricao });

                    foreach (var albumData in artistaData.Albuns)
                    {
                        // Cria o Álbum para o artista
                        var albumDto = await albumService.CriarAlbumAsync(new CriarAlbumRequestDto
                        {
                            Titulo = albumData.Titulo,
                            AnoLancamento = albumData.Ano,
                            ArtistaId = artistaDto.Id
                        });

                        foreach (var musicaData in albumData.Musicas)
                        {
                            // Cria a Música para o álbum
                            await musicaService.CriarMusicaAsync(new CriarMusicaRequestDto
                            {
                                Titulo = musicaData,
                                DuracaoSegundos = new Random().Next(180, 300), // Duração aleatória
                                AlbumId = albumDto.Id
                            });
                        }
                    }
                }
            }

            // --- 4. VÍNCULO DE GÊNEROS COM ARTISTAS E MÚSICAS ---
            var rock = await generoRepository.ObterPorNomeAsync("Rock Brasileiro");
            var mpb = await generoRepository.ObterPorNomeAsync("MPB");
            var metal = await generoRepository.ObterPorNomeAsync("Metal");

            if (rock != null && mpb != null && metal != null)
            {
                var legiao = await artistaRepository.ObterPorNomeAsync("Legião Urbana");
                if (legiao != null && !legiao.Generos.Any())
                {
                    legiao.AdicionarGenero(rock);
                    artistaRepository.Atualizar(legiao);
                }

                var caetano = await artistaRepository.ObterPorNomeAsync("Caetano Veloso");
                if (caetano != null && !caetano.Generos.Any())
                {
                    caetano.AdicionarGenero(mpb);
                    artistaRepository.Atualizar(caetano);
                }

                var sepultura = await artistaRepository.ObterPorNomeAsync("Sepultura");
                if (sepultura != null && !sepultura.Generos.Any())
                {
                    sepultura.AdicionarGenero(metal);
                    artistaRepository.Atualizar(sepultura);
                }

                // Adicionando gênero a algumas músicas para exemplo
                var quePais = await musicaRepository.ObterPorTituloAsync("Que País É Este");
                if (quePais != null && !quePais.Generos.Any())
                {
                    quePais.AdicionarGenero(rock);
                    musicaRepository.Atualizar(quePais);
                }

                await unitOfWork.SalvarAlteracoesAsync();
            }
        }

        // Método auxiliar para fornecer os dados de forma organizada
        private static List<ArtistaSeedData> GetArtistasData()
        {
            return new List<ArtistaSeedData>
        {
            new ArtistaSeedData("Legião Urbana", "Uma das bandas mais influentes do rock brasileiro.", new List<AlbumSeedData>
            {
                new AlbumSeedData("Dois", 1986, new List<string> { "Tempo Perdido", "Índios", "Eduardo e Mônica", "Quase Sem Querer", "Acrilic on Canvas" }),
                new AlbumSeedData("Que País É Este 1978/1987", 1987, new List<string> { "Que País É Este", "Faroeste Caboclo", "Eu Sei", "Angra dos Reis", "Mais do Mesmo" })
            }),
            new ArtistaSeedData("Caetano Veloso", "Ícone do movimento tropicalista.", new List<AlbumSeedData>
            {
                new AlbumSeedData("Transa", 1972, new List<string> { "You Don't Know Me", "Nine Out of Ten", "Triste Bahia", "It's a Long Way", "Mora na Filosofia" }),
                new AlbumSeedData("Cores, Nomes", 1982, new List<string> { "Queixa", "Ele me deu um beijo na boca", "Trem das Cores", "Sete mil vezes", "Coqueiro de Itapoã" })
            }),
            new ArtistaSeedData("Marisa Monte", "Aclamada por sua voz única e fusão de samba, pop e MPB.", new List<AlbumSeedData>
            {
                new AlbumSeedData("Memórias, Crônicas e Declarações de Amor", 2000, new List<string> { "Amor I Love You", "Não Vá Embora", "O Que Me Importa", "Gentileza", "A Sua" }),
                new AlbumSeedData("Universo ao Meu Redor", 2006, new List<string> { "Universo ao Meu Redor", "O Bonde do Dom", "Meu Canário", "Três Letrinhas", "Vai Saber?" })
            }),
            new ArtistaSeedData("Sepultura", "A banda brasileira de metal mais famosa no mundo.", new List<AlbumSeedData>
            {
                new AlbumSeedData("Roots", 1996, new List<string> { "Roots Bloody Roots", "Attitude", "Ratamahatta", "Spit", "Straighthate" }),
                new AlbumSeedData("Chaos A.D.", 1993, new List<string> { "Refuse/Resist", "Territory", "Slave New World", "Amen", "Kaiowas" })
            }),
            new ArtistaSeedData("Skank", "Banda mineira de pop rock.", new List<AlbumSeedData>
            {
                new AlbumSeedData("Calango", 1994, new List<string> { "Jackie Tequila", "Esmola", "O Beijo e a Reza", "Pacato Cidadão", "É Proibido Fumar" }),
                new AlbumSeedData("O Samba Poconé", 1996, new List<string> { "Garota Nacional", "Tão Seu", "É uma Partida de Futebol", "Poconé", "Eu Disse a Ela" })
            })
        };
        }
    }

    // Classes auxiliares para organizar os dados do seed
    internal class ArtistaSeedData
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public List<AlbumSeedData> Albuns { get; set; }
        public ArtistaSeedData(string nome, string desc, List<AlbumSeedData> albuns)
        {
            Nome = nome;
            Descricao = desc;
            Albuns = albuns;
        }
    }

    internal class AlbumSeedData
    {
        public string Titulo { get; set; }
        public int Ano { get; set; }
        public List<string> Musicas { get; set; }
        public AlbumSeedData(string titulo, int ano, List<string> musicas)
        {
            Titulo = titulo;
            Ano = ano;
            Musicas = musicas;
        }
    }
}
