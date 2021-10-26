using GraphQL.Types;
using GraphQLAPI.Infrastructure.DBContext;

namespace GraphQLAPI.GraphqlCore
{
    public class ParticipantType : ObjectGraphType<Participant>
    {
        public ParticipantType()
        {
            Field(x => x.ParticipantId).Description("Participant id.");
            Field(x => x.ParticipantName).Description("Participant name.");
            Field(x => x.Email).Description("Participant Email address.");
            Field(x => x.Phone).Description("Participant phone number.");
        }
    }
}
