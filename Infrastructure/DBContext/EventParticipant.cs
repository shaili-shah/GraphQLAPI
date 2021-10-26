using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLAPI.Infrastructure.DBContext
{
    public partial class EventParticipant
    {
        public int EventParticipantId { get; set; }
        public int EventId { get; set; }
        public int? ParticipantId { get; set; }

        public virtual TechEventInfo Event { get; set; }
        public virtual Participant Participant { get; set; }
    }
}
