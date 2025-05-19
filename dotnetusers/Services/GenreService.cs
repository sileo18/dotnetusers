using dotnetusers.Domain;
using dotnetusers.Repositories;

namespace dotnetusers.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        public async Task<Genre> AddAsync(Genre genre)
        {
            return await _genreRepository.AddAsync(genre);
        }
        public async Task<Genre> GetByIdAsync(int id)
        {
            return await _genreRepository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _genreRepository.GetAllAsync();
        }
    }    
}
