using GerenciadorDeTarefas.API.Entities;

namespace GerenciadorDeTarefas.API.Repositories
{
    public interface IUserRepository
    {      
        Task<User> GetUserByEmailAndPasswordAsync(string email, string passwordHash);
        Task<User> GetByIdAsync(int id);
    }
}
