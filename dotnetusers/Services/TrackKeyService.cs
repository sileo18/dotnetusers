using dotnetusers.Domain;
using dotnetusers.DTO_s;
using dotnetusers.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace dotnetusers.Services
{
    public class TrackKeyService : ITrackKeyService
    {
        private readonly ITrackKeyRepository _trackKeyRepository;

        public TrackKeyService(ITrackKeyRepository trackKeyRepository)
        {
            _trackKeyRepository = trackKeyRepository;
        }
        public Task<TrackKeys> AddAsync([FromBody] CreateTrackKeyDTO trackKeyDTO)
        {           

            TrackKeys trackKey = new TrackKeys
            {
                Nome = trackKeyDTO.Nome                
            };

            return _trackKeyRepository.AddAsync(trackKey);
        }

        public Task<TrackKeys> GetByIdAsync(int id)
        {
            TrackKeys trackKey = _trackKeyRepository.GetByIdAsync(id).Result;

            if (trackKey == null)
            {
                throw new Exception("Track key not found");
            } 
            
            return Task.FromResult(trackKey);
        }
    }
}
