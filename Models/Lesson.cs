namespace plat_kurs.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public int LessonNumber { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
