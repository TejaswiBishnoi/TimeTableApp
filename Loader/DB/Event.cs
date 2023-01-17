using System;
using System.Collections.Generic;

namespace Loader.DB
{
    public partial class Event
    {
        public Event()
        {
            Occurences = new HashSet<Occurence>();
        }

        public int EventId { get; set; }
        public string Name { get; set; } = null!;
        public bool IsCourse { get; set; }
        public bool IgnoreHoliday { get; set; }
        public string Owner { get; set; } = null!;

        public virtual Instructor OwnerNavigation { get; set; } = null!;
        public virtual Section? Section { get; set; }
        public virtual ICollection<Occurence> Occurences { get; set; }
    }
}
