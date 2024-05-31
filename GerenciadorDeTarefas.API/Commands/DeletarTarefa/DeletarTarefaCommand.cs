using MediatR;

namespace GerenciadorDeTarefas.API.Commands.DeletarTarefa
{
    public class DeletarTarefaCommand : IRequest<Unit>
    {
        public int Id { get; set; } 
    }
}
