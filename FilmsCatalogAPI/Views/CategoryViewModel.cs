namespace FilmsCatalogAPI.Views
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfFilms { get; set; }
        public int NestingLevel { get; set; }
    }
}
