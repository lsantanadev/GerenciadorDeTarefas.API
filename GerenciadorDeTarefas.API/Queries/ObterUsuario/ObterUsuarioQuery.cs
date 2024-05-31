using GerenciadorDeTarefas.API.ViewModels;
using MediatR;

namespace GerenciadorDeTarefas.API.Queries.ObterUsuario
{
    public class ObterUsuarioQuery : IRequest<UserViewModel>
    {
        public ObterUsuarioQuery(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
    }
}
