using GraphQL.Types;
using GraphQLAPI.Infrastructure.Repositories;

namespace GraphQLAPI.GraphqlCore
{
    public class TechEventQuery : ObjectGraphType<object>
    {
        public TechEventQuery(ITechEventRepository repository)
        {
            Name = "TechEventQuery";

            Field<TechEventInfoType>(
               "event",
               arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "eventId" }),
               resolve: context => repository.GetTechEventById(context.GetArgument<int>("eventId"))
            );

            Field<ListGraphType<TechEventInfoType>>(
             "events",
             resolve: context => repository.GetTechEvents()
          );

            Field<ParticipantType>(
            "participants",
            arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "participantId" }),
            resolve: context => repository.GetParticipantById(context.GetArgument<int>("participantId"))
         );

            Field<ListGraphType<ParticipantType>>(
            "allParticipants",
            resolve: context => repository.GetParticipant()
         );



        }
    }
}
