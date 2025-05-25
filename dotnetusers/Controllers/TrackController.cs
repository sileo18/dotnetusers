using dotnetusers.Domain;
using dotnetusers.DTO_s;
using dotnetusers.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnetusers.Controllers
{
    [Route("api/track")]
    public class TrackController : ControllerBase
    {
        private readonly ITrackService _trackService;
        public TrackController(ITrackService trackService)
        {
            _trackService = trackService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTrack([FromForm] CreateTrackDTO trackDTO)
        {
            if (trackDTO == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            Track result = await _trackService.AddAsync(trackDTO);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("{id}", Name = "GetTrackById")]
        public async Task<IActionResult> GetById(int id)
        {
            Track result = await _trackService.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("recent", Name = "GetMostRecent")]
        public async Task<IActionResult> GetMostRecent([FromQuery] int pageNumber, int pageSize)
        {
            
            var tracks = await _trackService.GetMostRecent(pageNumber, pageSize);
            return Ok(tracks);
        }

        [HttpGet("genre", Name = "GetTracksByGenresId")]
        public async Task<IActionResult> GetTracksByGenresId([FromQuery] int[] genresId, int pageNumber, int pageSize)
        {
            if (genresId == null || genresId.Length == 0)
            {
                return BadRequest("Invalid genres ID.");
            }

            var tracks = await _trackService.GetTracksByMultiplesGenresId(genresId, pageNumber, pageSize);

            if (tracks == null || !tracks.Any())
            {
                return NotFound("No tracks found for the provided genres ID.");
            }

            return Ok(tracks);

        }
    }
}
