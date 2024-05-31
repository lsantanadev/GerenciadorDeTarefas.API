using GerenciadorDeTarefas.API.ViewModels;
using MediatR;

namespace GerenciadorDeTarefas.API.Commands.LoginUsuario
{
    public class LoginUsuarioCommand : IRequest<LoginUserViewModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}