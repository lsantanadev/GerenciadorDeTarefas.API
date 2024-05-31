using MediatR;

namespace GerenciadorDeTarefas.API.Commands.AtualizarTarefa
{
    public class AtualizarTarefaCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Titulo { get; set; } 
        public string Descricao { get; set; }       
    }
}
