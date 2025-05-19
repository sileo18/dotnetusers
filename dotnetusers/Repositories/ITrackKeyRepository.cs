using dotnetusers.Domain;

namespace dotnetusers.Repositories
{
    public interface ITrackKeyRepository
    {
        Task<TrackKeys> AddAsync(TrackKeys trackKey);

        Task<TrackKeys> GetByIdAsync(int id);       

    }
}
