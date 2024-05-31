using GerenciadorDeTarefas.API.DataBase;
using GerenciadorDeTarefas.API.Entities;
using GerenciadorDeTarefas.API.Services;
using MediatR;


namespace GerenciadorDeTarefas.API.Commands
{
    public class CriarUsuarioCommandHandler : IRequestHandler<CriarUsuarioCommand, int>
    {
        private readonly GerenciadorDbContext _dbContext;
        private readonly IAuthService _authService;

        public CriarUsuarioCommandHandler(GerenciadorDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<int> Handle(CriarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _authService.ComputeSha256Hash(request.Password);
            var user = new User(request.FullName, request.Email, passwordHash, request.Role);

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user.Id;
        }                                       
    }
}
