using MediatR;
using GerenciadorDeTarefas.API.ViewModels;
using GerenciadorDeTarefas.API.DataBase;

namespace GerenciadorDeTarefas.API.Queries.ObterTarefaPorId
{
    public class ObterTarefaPorIdQueryHandler : IRequestHandler<ObterTarefaPorIdQuery, TaskViewModel>
    {
        private readonly GerenciadorDbContext _context;

        public ObterTarefaPorIdQueryHandler(GerenciadorDbContext context)
        {
            _context = context;
        }

        public async Task<TaskViewModel> Handle(ObterTarefaPorIdQuery request, CancellationToken cancellationToken)
        {
            var tarefa = await _context.Tasks.FindAsync(request.Id);

            if (tarefa == null)
                return null;

            var taskViewModel = new TaskViewModel
            (
                tarefa.Id,
                tarefa.Titulo,
                tarefa.Descricao             
            );

            return taskViewModel;
        }
    }
}
