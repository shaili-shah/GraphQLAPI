using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLAPI.Infrastructure.DBContext
{
    public partial class TechEventInfo
    {
        public TechEventInfo()
        {
            EventParticipants = new HashSet<EventParticipant>();
        }

        public int EventId { get; set; }
        public string EventName { get; set; }
        public string Speaker { get; set; }
        public DateTime EventDate { get; set; }

        public virtual ICollection<EventParticipant> EventParticipants { get; set; }
    }
}
