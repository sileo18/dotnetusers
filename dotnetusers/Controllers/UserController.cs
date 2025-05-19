using dotnetusers.Domain;
using dotnetusers.DTO_s;
using dotnetusers.Exceptions;
using dotnetusers.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnetusers.Controllers
{
    [ServiceFilter(typeof(GlobalExceptionHandler))]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsuarioService usuarioService;

        public UserController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Usuario>> create([FromBody] CreateUserDTO usuarioDTO)
        {
           if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var usuario = await usuarioService.AddAsync(usuarioDTO);
                ReturnUserDTO usuarioDTOed = new ReturnUserDTO
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,                    
                    Role = usuario.Roles.Select(r => r.Nome).ToList()
                };
                return CreatedAtAction(nameof(create), usuarioDTOed);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
