using System;
using System.Collections.Generic;

namespace Loader.DB
{
    public partial class Occurence
    {
        public int OccurenceId { get; set; }
        public int EventId { get; set; }
        public TimeOnly TimeBegin { get; set; }
        public TimeOnly TimeEnd { get; set; }
        public int Day { get; set; }
        public DateOnly DateStart { get; set; }
        public DateOnly DateEnd { get; set; }
        public string? RoomCode { get; set; }

        public virtual Event Event { get; set; } = null!;
        public virtual Room? RoomCodeNavigation { get; set; }
    }
}
