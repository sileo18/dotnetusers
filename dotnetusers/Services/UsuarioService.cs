using dotnetusers.Domain;
using dotnetusers.DTO_s;
using dotnetusers.Repositories;
using Microsoft.EntityFrameworkCore;

namespace dotnetusers.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUserRepository _userRepository;

        private readonly PasswordService _passwordService;

        public UsuarioService(IUserRepository userRepository, PasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        public async Task<Usuario> AddAsync(CreateUserDTO usuarioDTO)
        {

            var existingUser = await _userRepository.Context.Usuarios.FirstOrDefaultAsync(
                r => r.Email == usuarioDTO.Email);

            if (existingUser != null)
            {
                
                throw new ArgumentException("Email já cadastrado!");
               
            }

                var role = await _userRepository.Context.Roles.FirstOrDefaultAsync(
                r => r.Nome == usuarioDTO.Role);

            if (role == null)
            {
                throw new ArgumentException("Role não encontrada!");
            }

                Usuario novoUsuario = new Usuario
            {
                Nome = usuarioDTO.Nome,
                Email = usuarioDTO.Email,
                Passwordhashed = _passwordService.HashPassword(usuarioDTO.Senha),
                Active = usuarioDTO.Active,
                Roles = new List<Role> { role },
                };

            return await _userRepository.AddAsync(novoUsuario);
        }

        public async Task<Usuario?> AutenticarAsync(string email, string senha)
        {
            var usuario = await _userRepository.Context.Usuarios
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null)
                return null;

            if (!_passwordService.VerifyPassword(usuario.Passwordhashed, senha))
                return null;

            return usuario;
        }
    }
}
