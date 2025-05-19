using dotnetusers.Domain;
using dotnetusers.DTO_s;
using dotnetusers.Exceptions;
using dotnetusers.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace dotnetusers.Controllers
{
    [ServiceFilter(typeof(GlobalExceptionHandler))]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsuarioService usuarioService;
        private readonly IConfiguration Configuration;

        public UserController(IUsuarioService usuarioService, IConfiguration configuration)
        {
            this.usuarioService = usuarioService;
            Configuration = configuration;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Usuario>> create([FromBody] CreateUserDTO usuarioDTO)
        {
            if (!ModelState.IsValid)
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var usuario = await usuarioService.AutenticarAsync(login.Email, login.Senha);
            if (usuario == null)
                return Unauthorized("Credenciais inválidas.");

            var claims = new List<Claim>
            {
        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
        new Claim(ClaimTypes.Email, usuario.Email),
        
            };

            if(usuario.Roles != null)
            {
                foreach (var role in usuario.Roles)
                {
                   claims.Add(new Claim(ClaimTypes.Role, role.Nome));
                }
            }

            var loginJwtKey = Configuration["Jwt:Key"];
            var loginJwtIssuer = Configuration["Jwt:Issuer"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(loginJwtKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: loginJwtIssuer,
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
