using MediatR;
using System;


namespace GerenciadorDeTarefas.API.Commands
{
    public class CriarUsuarioCommand : IRequest<int>
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }       
    }
}
