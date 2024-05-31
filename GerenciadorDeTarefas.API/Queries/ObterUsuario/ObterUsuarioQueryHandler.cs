using GerenciadorDeTarefas.API.Repositories;
using GerenciadorDeTarefas.API.ViewModels;
using MediatR;   


namespace GerenciadorDeTarefas.API.Queries.ObterUsuario   
{  
    public class GetUserQueryHandler : IRequestHandler<ObterUsuarioQuery, UserViewModel>
    {
        private readonly IUserRepository _userRepository;  

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserViewModel> Handle(ObterUsuarioQuery request, CancellationToken cancellationToken)
        {
          
            var user = await _userRepository.GetByIdAsync(request.Id);
         
            if (user == null)
            {
                return null;
            }          
            return new UserViewModel(user.FullName, user.Email);
        }
    }
}
