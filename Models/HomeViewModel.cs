using Microsoft.AspNetCore.Mvc;

namespace plat_kurs.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Course> MyCourses { get; set; }
        public IEnumerable<Category> AllCategories { get; set; }
    }
}
