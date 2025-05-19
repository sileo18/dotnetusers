using dotnetusers.Domain;

namespace dotnetusers.Services
{
    public interface IGenreService
    {
        Task<Genre> AddAsync(Genre genre);
        Task<Genre> GetByIdAsync(int id);
        Task<IEnumerable<Genre>> GetAllAsync();
    }
}
