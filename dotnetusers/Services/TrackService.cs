using dotnetusers.Domain;
using dotnetusers.DTO_s;
using dotnetusers.Repositories;

namespace dotnetusers.Services
{
    public class TrackService : ITrackService
    {
        private readonly ITrackRepository _trackRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly S3Service _s3Service;

        public TrackService(ITrackRepository trackRepository, S3Service s3Service, IGenreRepository genreRepository)
        {
            _trackRepository = trackRepository;
            _s3Service = s3Service;
            _genreRepository = genreRepository;
        }

        public async Task<Track> AddAsync(CreateTrackDTO trackDTO)
        {
            string? imgUrl = null;
            string? audioUrl = null;

            if (trackDTO.Image != null && trackDTO.Image.Length > 0)
            {
                
                imgUrl = await _s3Service.UploadFileAsync(trackDTO.Image.OpenReadStream(), trackDTO.Image.FileName, trackDTO.Image.ContentType);

                if (imgUrl == null)
                {
                    
                    throw new Exception("Falha ao fazer upload da imagem.");
                }
            }
            else
            {                
                // imgUrl = "/default-track-image.png";
                throw new ArgumentException("Imagem da track é obrigatória.");
            }

            if (trackDTO.Audio != null && trackDTO.Audio.Length > 0)
            {
                
                audioUrl = await _s3Service.UploadFileAsync(trackDTO.Audio.OpenReadStream(), trackDTO.Audio.FileName, trackDTO.Audio.ContentType);
                if (audioUrl == null)
                {
                    
                    throw new Exception("Falha ao fazer upload do áudio.");
                }
            }
            else
            {
                throw new ArgumentException("Arquivo de áudio da track é obrigatório.");
            }

            Track track = new Track
            {
                Nome = trackDTO.Name,
                Bpm = trackDTO.Bpm,
                ImageUrl = imgUrl,
                AudioUrl = audioUrl,
                CreatedAt = DateTime.UtcNow,
                UsuarioId = trackDTO.UserId,                
                KeyId = trackDTO.KeyId

            };

            if (trackDTO.GenreId != null && trackDTO.GenreId.Count > 0)
            {
                foreach (var genreId in trackDTO.GenreId)
                {
                    var genre = await _genreRepository.GetByIdAsync(genreId);
                    if (genre != null)
                    {
                        track.Genres.Add(genre);
                    }
                    else
                    {
                        throw new ArgumentException($"Gênero com ID {genreId} não encontrado.");
                    }
                }
            }


            return await _trackRepository.AddAsync(track);
        }

        public Task<bool> DeleteAsync(int id)
        {
           return _trackRepository.DeleteAsync(id);
        }

        public Task<Track> GetByIdAsync(int id)
        {
            Task<Track> track = _trackRepository.GetByIdAsync(id);

            if (track == null)
            {
                throw new Exception("Track not found");
            }

            return track;
        }

        public Task<IEnumerable<Track>> GetMostRecent(int pageNumber = 1, int pageSize = 10)
        {
            Task<IEnumerable<Track>> tracks = _trackRepository.GetMostRecent(pageNumber, pageSize);

            if (tracks == null)
            {
                throw new Exception("Tracks not found");
            }

            return tracks;
        }
    }
}
