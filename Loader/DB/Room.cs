using System;
using System.Collections.Generic;

namespace Loader.DB
{
    public partial class Room
    {
        public Room()
        {
            Occurences = new HashSet<Occurence>();
        }

        public string RoomCode { get; set; } = null!;
        public string? Building { get; set; }
        public string? Type { get; set; }
        public int Capacity { get; set; }

        public virtual ICollection<Occurence> Occurences { get; set; }
    }
}
