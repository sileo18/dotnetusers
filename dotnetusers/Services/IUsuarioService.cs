using dotnetusers.Domain;
using dotnetusers.DTO_s;

namespace dotnetusers.Services
{
    public interface IUsuarioService
    {
        Task<Usuario> AddAsync(CreateUserDTO usuarioDTO);

        Task<Usuario?> AutenticarAsync(string email, string senha);
    }
}
