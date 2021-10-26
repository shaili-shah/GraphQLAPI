using System.Collections.Generic;

#nullable disable

namespace GraphQLAPI.Infrastructure.DBContext
{
    public partial class Participant
    {
        public Participant()
        {
            EventParticipants = new HashSet<EventParticipant>();
        }

        public int ParticipantId { get; set; }
        public string ParticipantName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<EventParticipant> EventParticipants { get; set; }
    }
}
