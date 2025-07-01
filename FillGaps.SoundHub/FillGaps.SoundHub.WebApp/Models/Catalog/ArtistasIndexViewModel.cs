namespace FillGaps.SoundHub.WebApp.Models.Catalog
{
    public class ArtistasIndexViewModel
    {
        public IEnumerable<ArtistaViewModel> TodosArtistas { get; set; } = new List<ArtistaViewModel>();

        public HashSet<Guid> ArtistasFavoritosIds { get; set; } = new HashSet<Guid>();
    }
}
