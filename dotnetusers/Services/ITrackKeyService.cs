using dotnetusers.Domain;
using dotnetusers.DTO_s;

namespace dotnetusers.Services
{
    public interface ITrackKeyService
    {
        Task<TrackKeys> AddAsync(CreateTrackKeyDTO trackKeyDTO);

        Task<TrackKeys> GetByIdAsync(int id);
    }
}
