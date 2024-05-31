using MediatR;
using GerenciadorDeTarefas.API.DataBase;
using GerenciadorDeTarefas.API.ViewModels;

namespace GerenciadorDeTarefas.API.Queries.ObterTarefaPorTitulo
{
    public class ObterTarefaPorTituloQueryHandler : IRequestHandler<ObterTarefaPorTituloQuery, List<TaskViewModel>>
    {
        private readonly GerenciadorDbContext _context;

        public ObterTarefaPorTituloQueryHandler(GerenciadorDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskViewModel>> Handle(ObterTarefaPorTituloQuery request, CancellationToken cancellationToken)
        {
            var tarefas = _context.Tasks
                .Where(t => t.Titulo.Contains(request.Titulo))
                .Select(t => new TaskViewModel(t.Id, t.Titulo, t.Descricao))
                .ToList();

            return await Task.FromResult(tarefas);
        }
    }
}
