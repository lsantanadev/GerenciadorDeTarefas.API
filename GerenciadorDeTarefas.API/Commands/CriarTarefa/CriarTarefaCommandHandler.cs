using MediatR;
using GerenciadorDeTarefas.API.DataBase;
using GerenciadorDeTarefas.API.Models;

namespace GerenciadorDeTarefas.API.Commands
{
    public class CriarTarefaCommandHandler : IRequestHandler<CriarTarefaCommand, int>
    {
        private readonly GerenciadorDbContext _context;

        public CriarTarefaCommandHandler(GerenciadorDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CriarTarefaCommand request, CancellationToken cancellationToken)
        {
            var novaTarefa = new TaskModel
            {
                Titulo = request.Titulo,
                Descricao = request.Descricao,             
            };

            _context.Tasks.Add(novaTarefa);
            await _context.SaveChangesAsync();

            return novaTarefa.Id;
        }
    }
}
