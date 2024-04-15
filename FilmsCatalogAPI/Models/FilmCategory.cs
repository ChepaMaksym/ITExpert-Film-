using System.ComponentModel.DataAnnotations;

namespace FilmsCatalogAPI.Models
{
    public class FilmCategory
    {
        public int Id { get; set; }
        [Required]
        public int FilmId { get; set; }
        public Film? Film { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
