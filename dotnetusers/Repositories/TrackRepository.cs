using dotnetusers.Domain;
using Microsoft.EntityFrameworkCore;

namespace dotnetusers.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationDbContext ApplicationDbContext => _context;

        public TrackRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Track> AddAsync(Track track)
        {
             _context.AddAsync(track);
            await _context.SaveChangesAsync();
            return track;
        }

        public Task<bool> DeleteAsync(int id)
        {
            var track = _context.Tracks.Find(id);
            if (track == null)
            {
                return Task.FromResult(false);
            }
            _context.Tracks.Remove(track);
            _context.SaveChangesAsync();
            return Task.FromResult(true);
            
        }

        public async Task<Track> GetByIdAsync(int id)
        {
            return await _context.Tracks.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Track>> GetMostRecent(int pageNumber = 1, int pageSize = 10)
        {
            return await _context.Tracks
                .Include(t => t.Genres)
         .OrderByDescending(t => t.CreatedAt)
         .Skip((pageNumber - 1) * pageSize)
         .Take(pageSize)
         .ToListAsync();
        }

        public Task<Track> UpdateAsync(Track track)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Track>> GetTracksByUserId(int userId, int pageNumber = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Track>> GetTracksByMultiplesGenresId(int[] genresId, int pageNumber = 1, int pageSize = 10)
        {
            return await _context.Tracks
                .Include(t => t.Genres)
                .Where(t => t.Genres.Any(g => genresId.Contains(g.Id)))
                .OrderByDescending(t => t.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
