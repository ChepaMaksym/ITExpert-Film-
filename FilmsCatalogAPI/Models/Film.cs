using System.ComponentModel.DataAnnotations;

namespace FilmsCatalogAPI.Models
{
    public class Film
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Director { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
        public ICollection<FilmCategory>? FilmCategory { get; set; }
    }
}
