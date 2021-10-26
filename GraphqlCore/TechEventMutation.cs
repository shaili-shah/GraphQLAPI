﻿using GraphQL;
using GraphQL.Types;
using GraphQLAPI.Infrastructure.DBContext;
using GraphQLAPI.Infrastructure.Repositories;
using GraphQLAPI.Models;

namespace GraphQLAPI.GraphqlCore
{
    public class TechEventMutation : ObjectGraphType<object>
    {
        public TechEventMutation(ITechEventRepository repository)
        {
            Name = "TechEventMutation";

            FieldAsync<TechEventInfoType>(
                "createTechEvent",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<TechEventInputType>> { Name = "techEventInput" }
                ),
                resolve: async context =>
                {
                    var techEventInput = context.GetArgument<NewTechEventRequest>("techEventInput");
                    return await repository.AddTechEventAsync(techEventInput);
                });

            FieldAsync<TechEventInfoType>(
            "updateTechEvent",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<TechEventInputType>> { Name = "techEventInput" },
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "techEventId" }),
            resolve: async context =>
            {
                var techEventInput = context.GetArgument<TechEventInfo>("techEventInput");
                var techEventId = context.GetArgument<int>("techEventId");

                var eventInfoRetrived = await repository.GetTechEventById(techEventId);
                if (eventInfoRetrived == null)
                {
                    context.Errors.Add(new ExecutionError("Couldn't find Tech event info."));
                    return null;
                }
                eventInfoRetrived.EventName = techEventInput.EventName;
                eventInfoRetrived.EventDate = techEventInput.EventDate;
                eventInfoRetrived.Speaker = techEventInput.Speaker;

                return await repository.UpdateTechEventAsync(eventInfoRetrived);
            }
        );


        FieldAsync<StringGraphType>(
        "deleteTechEvent",
        arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "techEventId" }),
        resolve: async context =>
        {
            var techEventId = context.GetArgument<int>("techEventId");

            var eventInfoRetrived = await repository.GetTechEventById(techEventId);
            if (eventInfoRetrived == null)
            {
                context.Errors.Add(new ExecutionError("Couldn't find Tech event info."));
                return null;
            }
            await repository.DeleteTechEventByIdAsync(eventInfoRetrived);
            return $"Tech Event ID {techEventId} with Name {eventInfoRetrived.EventName} has been deleted succesfully.";
        }
    );



        }
    }
}