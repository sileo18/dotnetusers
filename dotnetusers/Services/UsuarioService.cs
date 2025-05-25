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

        private readonly S3Service _s3Service;

        public UsuarioService(IUserRepository userRepository, PasswordService passwordService, S3Service s3Service)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _s3Service = s3Service;
        }

        public async Task<Usuario> AddAsync(CreateUserDTO usuarioDTO)
        {

            String imgUrl = "null";

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

            if (usuarioDTO.Image != null && usuarioDTO.Image.Length > 0)
            {

                imgUrl = await _s3Service.UploadFileAsync(usuarioDTO.Image.OpenReadStream(), usuarioDTO.Image.FileName, usuarioDTO.Image.ContentType);

                if (imgUrl == null)
                {

                    throw new Exception("Falha ao fazer upload da imagem.");
                }
            }
            else
            {
                
                throw new ArgumentException("Imagem da track é obrigatória.");
            }

            Usuario novoUsuario = new Usuario
            {
                Nome = usuarioDTO.Nome,
                Email = usuarioDTO.Email,
                Passwordhashed = _passwordService.HashPassword(usuarioDTO.Senha),
                Active = usuarioDTO.Active,
                Roles = new List<Role> { role },
                ImageUrl = imgUrl,
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
