using dotnetusers.Domain;
using dotnetusers.DTO_s;
using dotnetusers.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace dotnetusers.Controllers
{
    [Route("api/key")]
    public class TrackKeyController : ControllerBase
    {
        private readonly ITrackKeyService _trackKeyService;

        public TrackKeyController(ITrackKeyService trackKeyService)
        {
            _trackKeyService = trackKeyService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateTrackKeyDTO trackKeyDTO)
        {
            if (trackKeyDTO == null)
            {
                return BadRequest("Invalid data.");
            }

            if(ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            TrackKeys result = await _trackKeyService.AddAsync(trackKeyDTO);

            return CreatedAtAction(
                 nameof(GetById),
                 new {id = result.Id},
                 result);           
        }

        [HttpGet("{id}", Name = "GetKeyById")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID.");
            }
            TrackKeys trackKey = await _trackKeyService.GetByIdAsync(id);

            if (trackKey == null)
            {
                return NotFound("Track key not found.");
            }
            return Ok(trackKey);
        }
    }
}
