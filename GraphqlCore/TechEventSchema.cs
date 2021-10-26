using GraphQL;
using GraphQL.Types;

namespace GraphQLAPI.GraphqlCore
{
    public class TechEventSchema : Schema
    {
        public TechEventSchema(IDependencyResolver resolver)
        {
            Query = resolver.Resolve<TechEventQuery>();
            Mutation = resolver.Resolve<TechEventMutation>();
            DependencyResolver = resolver;
        }
    }
}
