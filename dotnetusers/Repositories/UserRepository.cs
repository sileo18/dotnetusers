using dotnetusers.Domain;

namespace dotnetusers.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationDbContext Context => _context;

        public UserRepository(ApplicationDbContext context)
        {
                       _context = context;
        }

        public async Task<Usuario> AddAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public Task<Usuario> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
