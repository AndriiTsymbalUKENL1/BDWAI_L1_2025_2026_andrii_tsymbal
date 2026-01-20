using System.ComponentModel.DataAnnotations;

namespace plat_kurs.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tytuł jest wymagany")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Range(0, 10000)]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public ICollection<Lesson>? Lessons { get; set; }
    }
}
