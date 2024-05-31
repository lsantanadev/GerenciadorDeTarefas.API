using GerenciadorDeTarefas.API.DataBase;
using GerenciadorDeTarefas.API.Entities;
using Microsoft.EntityFrameworkCore;


namespace GerenciadorDeTarefas.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GerenciadorDbContext _dbContext;

        public UserRepository(GerenciadorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string passwordHash)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == passwordHash);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
        }
    }
}
