using MediatR;
using GerenciadorDeTarefas.API.DataBase;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeTarefas.API.Commands.AtualizarTarefa
{
    public class AtualizarTarefaCommandHandler : IRequestHandler<AtualizarTarefaCommand, Unit>
    {
        private readonly GerenciadorDbContext _context;

        public AtualizarTarefaCommandHandler(GerenciadorDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AtualizarTarefaCommand request, CancellationToken cancellationToken)
        {
            var tarefa = await _context.Tasks.FindAsync(request.Id);

            if (tarefa == null)
            {
                throw new Exception("Tarefa não encontrada");
            }

            tarefa.Titulo = request.Titulo;
            tarefa.Descricao = request.Descricao;          

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {

                throw new Exception("Ocorreu um erro ao salvar as alterações..", ex);
            }

            return Unit.Value;
        }
    }
}
