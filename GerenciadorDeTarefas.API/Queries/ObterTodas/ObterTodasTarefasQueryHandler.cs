using GerenciadorDeTarefas.API.DataBase;
using GerenciadorDeTarefas.API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.API.Queries.ObterTodasTarefas
{
    public class ObterTodasTarefasQueryHandler : IRequestHandler<ObterTodasTarefasQuery, IEnumerable<TaskModel>>
    {
        private readonly GerenciadorDbContext _context;

        public ObterTodasTarefasQueryHandler(GerenciadorDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskModel>> Handle(ObterTodasTarefasQuery request, CancellationToken cancellationToken)
        {
            return await _context.Tasks.ToListAsync();
        }
    }
}
