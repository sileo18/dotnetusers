using dotnetusers.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace dotnetusers.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationDbContext Context => _context;

        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Genre> AddAsync(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
            return genre;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return false;
            }
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ExistsAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);

            if(genre == null)
            {
                return false;
            }

            return true;
        }
        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _context.Genres.ToListAsync();
        }
        public async Task<Genre> GetByIdAsync(int id)
        {
            return await _context.Genres.FindAsync(id);
        }
        public Task<bool> UpdateAsync(Genre genre)
        {
            throw new NotImplementedException();
        }
    }
    
}
