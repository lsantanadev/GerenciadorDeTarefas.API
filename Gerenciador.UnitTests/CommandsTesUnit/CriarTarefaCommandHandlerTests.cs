using GerenciadorDeTarefas.API.Commands;
using GerenciadorDeTarefas.API.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador.UnitTests.CommandsTesUnit
{
    public class CriarTarefaCommandHandlerTests
    {
        [Fact]
            public async Task DeveAdicionarTarefaERetornarIdDaTarefa()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GerenciadorDbContext>()
                .UseInMemoryDatabase(databaseName: "DeveAdicionarTarefaERetornarIdDaTarefa")
                .Options;
            var context = new GerenciadorDbContext(options);

            var handler = new CriarTarefaCommandHandler(context);
            var command = new CriarTarefaCommand { Titulo = "Nova Tarefa", Descricao = "Descrição da Tarefa" };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            var task = await context.Tasks.FindAsync(result);
            Assert.NotNull(task);
            Assert.Equal("Nova Tarefa", task.Titulo);
            Assert.Equal("Descrição da Tarefa", task.Descricao);
        }

    }
}
