namespace FillGaps.SoundHub.WebApp.Models.Catalog
{
    public class MusicasIndexViewModel
    {
        public IEnumerable<MusicaViewModel> TodasMusicas { get; set; } = new List<MusicaViewModel>();
        public HashSet<Guid> MusicasFavoritasIds { get; set; } = new HashSet<Guid>();
    }
}
