using MediatR;
using System.Collections.Generic;
using GerenciadorDeTarefas.API.Models;

namespace GerenciadorDeTarefas.API.Queries.ObterTodasTarefas
{
    public class ObterTodasTarefasQuery : IRequest<IEnumerable<TaskModel>>
    {
    }
}
