using System;
using System.Collections.Generic;

namespace Loader.DB
{
    public partial class Course
    {
        public Course()
        {
            Sections = new HashSet<Section>();
            Instrucors = new HashSet<Instructor>();
        }

        public string CourseCode { get; set; } = null!;
        public string CourseName { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Type { get; set; } = null!;
        public decimal TotalCredits { get; set; }
        public decimal LectureCredits { get; set; }
        public decimal TutorialCredits { get; set; }
        public decimal PracticalCredits { get; set; }
        public string CoordinatorId { get; set; } = null!;
        public string Department { get; set; } = null!;

        public virtual Instructor Coordinator { get; set; } = null!;
        public virtual ICollection<Section> Sections { get; set; }

        public virtual ICollection<Instructor> Instrucors { get; set; }
    }
}
