using dotnetusers.Domain;
using dotnetusers.DTO_s;
using dotnetusers.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnetusers.Controllers
{
    [Route("api/genre")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateGenreDTO genre)
        {
            if (genre == null)
            {
                return BadRequest("Invalid data.");
            }
            if(ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            Genre genreEntity = new Genre
            {
                Nome = genre.Nome                
            };
            Genre result = await _genreService.AddAsync(genreEntity);
            return CreatedAtAction(
                nameof(GetById),
                new {id = result.Id},
                result);
        }

        [HttpGet("{id}", Name = "GetGenreById")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID.");
            }
            Genre genre = await _genreService.GetByIdAsync(id);
            if (genre == null)
            {
                return NotFound("Genre not found.");
            }
            return Ok(genre);
        }
    }
}
