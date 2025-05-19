using dotnetusers.Domain;

namespace dotnetusers.Repositories
{
    public interface IGenreRepository
    {
        Task<Genre> AddAsync(Genre genre);
        Task<Genre> GetByIdAsync(int id);
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<bool> UpdateAsync(Genre genre);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
