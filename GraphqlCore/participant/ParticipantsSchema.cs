using GraphQL;
using GraphQL.Types;

namespace GraphQLAPI.GraphqlCore.participant
{
    public class ParticipantsSchema : Schema
    {
        public ParticipantsSchema(IDependencyResolver resolver)
        {
            Query = resolver.Resolve<ParticipantQuery>();
            DependencyResolver = resolver;
        }
    }
}