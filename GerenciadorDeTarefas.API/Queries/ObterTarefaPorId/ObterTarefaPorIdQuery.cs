using MediatR;
using GerenciadorDeTarefas.API.ViewModels;

namespace GerenciadorDeTarefas.API.Queries
{
    public class ObterTarefaPorIdQuery : IRequest<TaskViewModel>
    {
        public int Id { get; private set; }

        public ObterTarefaPorIdQuery(int id)
        {
            Id = id;
        }
    }
}
