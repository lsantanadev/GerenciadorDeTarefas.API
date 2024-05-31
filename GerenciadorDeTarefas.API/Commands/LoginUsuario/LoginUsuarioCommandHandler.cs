using GerenciadorDeTarefas.API.Repositories;
using GerenciadorDeTarefas.API.Services;
using GerenciadorDeTarefas.API.ViewModels;
using MediatR;


namespace GerenciadorDeTarefas.API.Commands.LoginUsuario
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUsuarioCommand, LoginUserViewModel>
    {

        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public LoginUserCommandHandler(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<LoginUserViewModel> Handle(LoginUsuarioCommand request, CancellationToken cancellationToken)
        {
            
            var passwordHash = _authService.ComputeSha256Hash(request.Password);          
            var user = await _userRepository.GetUserByEmailAndPasswordAsync(request.Email, passwordHash);
           
            if (user == null)
            {
                return null;
            }
            
            var token = _authService.GenerateJwtToken(user.Email, user.Role);
        
            return new LoginUserViewModel(user.Email, token);
        }

    }
}
