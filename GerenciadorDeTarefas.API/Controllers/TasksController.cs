using MediatR;
using Microsoft.AspNetCore.Mvc;
using GerenciadorDeTarefas.API.Queries;
using GerenciadorDeTarefas.API.Queries.ObterTarefaPorId;
using GerenciadorDeTarefas.API.Queries.ObterTarefaPorTitulo;
using GerenciadorDeTarefas.API.Commands.AtualizarTarefa;
using GerenciadorDeTarefas.API.Commands.DeletarTarefa;
using GerenciadorDeTarefas.API.Commands;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorDeTarefas.API.Queries.ObterTodasTarefas;

namespace GerenciadorDeTarefas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CriarTarefa")]
        [Authorize(Roles = "gerente, funcionario")]
        public async Task<IActionResult> Criar([FromBody] CriarTarefaCommand command)
        {
            var id = await _mediator.Send(command);

            var response = new
            {
                message = "Tarefa criada com sucesso.",
                id = id
            };

            return CreatedAtAction(nameof(ObterPorId), new { id = id }, response);
        }

        [HttpPut("Atualizar/{id}")]
        [Authorize(Roles = "gerente, funcionario")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarTarefaCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("Deletar")]
        [Authorize (Roles = "gerente")]
        public async Task<IActionResult> Deletar(int id)
        {
            var command = new DeletarTarefaCommand { Id = id };
            var result = await _mediator.Send(command);
            if (result == null)
                return NotFound();

            return NoContent();
        }

        [HttpGet("ObterPorTitulo/{titulo}")]
        [Authorize(Roles = "gerente, funcionario")]
        public async Task<IActionResult> ObterPorTitulo(string titulo)
        {
            var query = new ObterTarefaPorTituloQuery(titulo);
            var result = await _mediator.Send(query);

            if (result == null || !result.Any())
                return NotFound();

            return Ok(result);
        }

        [HttpGet("ObterPorID")]
        [Authorize(Roles = "gerente, funcionario")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var query = new ObterTarefaPorIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("ObterTodas")]
        [Authorize(Roles = "gerente, funcionario")]
        public async Task<IActionResult> ObterTodas()
        {
            var query = new ObterTodasTarefasQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
