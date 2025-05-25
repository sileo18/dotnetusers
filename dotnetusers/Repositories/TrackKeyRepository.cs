using dotnetusers.Domain;
using Microsoft.EntityFrameworkCore;

namespace dotnetusers.Repositories
{
    public class TrackKeyRepository : ITrackKeyRepository
    {
        private readonly ApplicationDbContext _context;
        public ApplicationDbContext Context => _context;
        public TrackKeyRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TrackKeys> AddAsync(TrackKeys trackKey)
        {
            await _context.TrackKeys.AddAsync(trackKey);
            await _context.SaveChangesAsync();
            return trackKey;
        }
        public async Task<TrackKeys?> GetByIdAsync(int id) 
        {
            TrackKeys? key = await _context.TrackKeys.FirstOrDefaultAsync(t => t.Id == id); 
            return key;
        }
    }
}
