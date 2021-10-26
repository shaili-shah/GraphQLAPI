using GraphQLAPI.Infrastructure.DBContext;
using GraphQLAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQLAPI.Infrastructure.Repositories
{
    public interface ITechEventRepository
    {
        Task<TechEventInfo[]> GetTechEvents();

        Task<Participant[]> GetParticipant();

        Task<TechEventInfo> GetTechEventById(int id);
        
        Task<List<Participant>> GetParticipantInfoByEventId(int id);

        Participant GetParticipantById(int participantId);

        Task<TechEventInfo> AddTechEventAsync(NewTechEventRequest techEvent);

        Task<Participant> AddParticipant(NewParticipantRequest model);

        Task<TechEventInfo> UpdateTechEventAsync(TechEventInfo techEvent);
        
        Task<bool> DeleteTechEventByIdAsync(TechEventInfo techEvent);
    }
}