using dotnetusers.Domain;
using dotnetusers.DTO_s;

namespace dotnetusers.Services
{
    public interface ITrackService
    {
        Task<Track> AddAsync(CreateTrackDTO trackDTO);
        Task<Track> GetByIdAsync(int id);
        Task<IEnumerable<Track>> GetMostRecent(int pageNumber = 1, int pageSize = 10);
        //Task<Track> UpdateAsync(UpdateTrackDTO trackDTO);
        Task<bool> DeleteAsync(int id);
    }
}
