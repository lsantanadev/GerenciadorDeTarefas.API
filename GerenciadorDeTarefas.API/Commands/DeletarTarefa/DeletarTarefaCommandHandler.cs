using MediatR;
using GerenciadorDeTarefas.API.DataBase;


namespace GerenciadorDeTarefas.API.Commands.DeletarTarefa
{
    public class DeletarTarefaCommandHandler : IRequestHandler<DeletarTarefaCommand, Unit>
    {
        private readonly GerenciadorDbContext _context;

        public DeletarTarefaCommandHandler(GerenciadorDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeletarTarefaCommand request, CancellationToken cancellationToken)
        {
            var tarefa = await _context.Tasks.FindAsync(request.Id);

            if (tarefa != null)
            {
                _context.Tasks.Remove(tarefa);
                await _context.SaveChangesAsync();
            }

            return Unit.Value;
        }
    }
}
