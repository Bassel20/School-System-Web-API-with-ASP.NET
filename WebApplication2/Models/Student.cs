using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Student
    {
        public Student()
        {
            this.Courses = new HashSet<Course>();
        }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
    public class AcademicYear
    {
        public int YearId { get; set; }
        public string YearName { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
    public class Course
    {
        public Course()
        {
            this.Students = new HashSet<Student>();
        }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
    public class Grade
    {
        public int GradeId { get; set; }
        public int GradeValue { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
