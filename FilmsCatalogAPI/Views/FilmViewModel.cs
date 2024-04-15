namespace FilmsCatalogAPI.Views
{
    public class FilmViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IEnumerable<string> Categories { get; set; }
    }
}
