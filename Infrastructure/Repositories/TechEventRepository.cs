using GraphQLAPI.Infrastructure.DBContext;
using GraphQLAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLAPI.Infrastructure.Repositories
{
    public class TechEventRepository : ITechEventRepository
    {
        private readonly TechEventDBContext _context;

        public TechEventRepository(TechEventDBContext context)
        {
            _context = context;
        }

        #region get

        public async Task<List<Participant>> GetParticipantInfoByEventId(int id)
        {
            return await (from ep in _context.EventParticipants
                          join p in this._context.Participants on ep.ParticipantId equals p.ParticipantId
                          where ep.EventId == id
                          select p).ToListAsync();
        }

        public async Task<TechEventInfo> GetTechEventById(int id)
        {
            return await Task.FromResult(_context.TechEventInfos.FirstOrDefault(i => i.EventId == id));
        }

        public async Task<TechEventInfo[]> GetTechEvents()
        {
            return _context.TechEventInfos.ToArray();
        }

        public async Task<Participant[]> GetParticipant()
        {
            return _context.Participants.ToArray();
        }
        

        public Participant GetParticipantById(int participantId)
        {
            return _context.Participants.FirstOrDefault(x => x.ParticipantId == participantId);
        }

        #endregion

        public async Task<TechEventInfo> AddTechEventAsync(NewTechEventRequest techEvent)
        {
            var newEvent = new TechEventInfo { EventName = techEvent.EventName, Speaker = techEvent.Speaker, EventDate = techEvent.EventDate };
            var savedEvent = (await _context.TechEventInfos.AddAsync(newEvent)).Entity;
            await _context.SaveChangesAsync();
            return savedEvent;
        }

        public async Task<Participant> AddParticipant(NewParticipantRequest model)
        {
            var newParticipant = new Participant { ParticipantName = model.ParticipantName, Email = model.Email , Phone = model.Phone };
            var savedEvent = (await _context.Participants.AddAsync(newParticipant)).Entity;
            await _context.SaveChangesAsync();
            return savedEvent;
        }

        public async Task<bool> DeleteTechEventByIdAsync(TechEventInfo techEvent)
        {
            _context.TechEventInfos.Remove(techEvent);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TechEventInfo> UpdateTechEventAsync(TechEventInfo techEvent)
        {
            var updatedEvent = (_context.TechEventInfos.Update(techEvent)).Entity;
            await _context.SaveChangesAsync();
            return updatedEvent;
        }



    }
}
