namespace plat_kurs.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public string UserId { get; set; }

        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
