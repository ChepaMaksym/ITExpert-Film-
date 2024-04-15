using System.ComponentModel.DataAnnotations;

namespace FilmsCatalogAPI.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Display(Name = "Parent Category")]
        public int? ParentCategoryId { get; set; }
        public ICollection<FilmCategory>? FilmCategory { get; set; }
    }
}
