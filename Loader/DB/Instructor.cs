using System;
using System.Collections.Generic;

namespace Loader.DB
{
    public partial class Instructor
    {
        public Instructor()
        {
            Courses = new HashSet<Course>();
            Events = new HashSet<Event>();
            CourseCodes = new HashSet<Course>();
            Sections = new HashSet<Section>();
        }

        public string InstructorId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string EmailId { get; set; } = null!;
        public string Department { get; set; } = null!;

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<Course> CourseCodes { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
    }
}
