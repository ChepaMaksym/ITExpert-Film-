namespace FilmsCatalogAPI.Views
{
    public class UpdateFilmCategorizerModel
    {
        public int FilmId { get; set; }
        public IEnumerable<int> CategoryIds { get; set; }
    }
}
