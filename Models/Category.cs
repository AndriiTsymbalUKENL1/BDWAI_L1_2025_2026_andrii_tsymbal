using System.ComponentModel.DataAnnotations;

namespace plat_kurs.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa kategorii jest wymagana")]
        public string Name { get; set; }

        public ICollection<Course>? Courses { get; set; }
    }
}

