using dotnetusers.Domain;

namespace dotnetusers.Repositories
{
    public interface IUserRepository
    {

        Task<Usuario> AddAsync(Usuario usuario);

        Task<Usuario> GetByIdAsync(int id);

        ApplicationDbContext Context { get; }
    }
}
