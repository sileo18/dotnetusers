using dotnetusers.Domain;

namespace dotnetusers.Repositories
{
    public interface ITrackRepository
    {
        Task<Track> AddAsync(Track track);
        Task<Track> GetByIdAsync(int id);
        Task<IEnumerable<Track>> GetMostRecent(int pageNumber = 1, int pageSize = 10);
        Task<Track> UpdateAsync(Track track);
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<Track>> GetTracksByUserId(int userId, int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<Track>> GetTracksByMultiplesGenresId(int[] genresId, int pageNumber = 1, int pageSize = 10);
    }
}
