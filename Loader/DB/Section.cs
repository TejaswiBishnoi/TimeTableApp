using System;
using System.Collections.Generic;

namespace Loader.DB
{
    public partial class Section
    {
        public Section()
        {
            Instructors = new HashSet<Instructor>();
        }

        public string SectionId { get; set; } = null!;
        public string CourseCode { get; set; } = null!;
        public int EventId { get; set; }
        public string Name { get; set; } = null!;

        public virtual Course CourseCodeNavigation { get; set; } = null!;
        public virtual Event Event { get; set; } = null!;

        public virtual ICollection<Instructor> Instructors { get; set; }
    }
}
