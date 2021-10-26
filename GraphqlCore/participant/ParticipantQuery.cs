using GraphQL.Types;
using GraphQLAPI.Infrastructure.Repositories;

namespace GraphQLAPI.GraphqlCore.participant
{
    public class ParticipantQuery :  ObjectGraphType<object> 
    {
        public ParticipantQuery(ITechEventRepository repository)
        {
            Name = "ParticipantQuery";

            Field<ListGraphType<ParticipantType>>(
             "participants",
             resolve: context => repository.GetParticipant()
          );
        }
    }
}