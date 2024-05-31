using MediatR;
using GerenciadorDeTarefas.API.ViewModels;

namespace GerenciadorDeTarefas.API.Queries.ObterTarefaPorTitulo
{
    public class ObterTarefaPorTituloQuery : IRequest<List<TaskViewModel>>
    {
        public string Titulo { get; set; }

        public ObterTarefaPorTituloQuery(string titulo)
        {
            Titulo = titulo;
        }
    }
}
