using GerenciadorDeTarefas.API.Commands.DeletarTarefa;
using GerenciadorDeTarefas.API.DataBase;
using GerenciadorDeTarefas.API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Gerenciador.UnitTests.CommandsTesUnit
{
    public class DeletarTarefaCommandHandlerTests
    {
        [Fact]
        public async Task DeletarTarefaExistente_RetornarUnit()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GerenciadorDbContext>()
                .UseInMemoryDatabase(databaseName: "GerenciadorDb")
                .Options;

            await using var context = new GerenciadorDbContext(options);
            var tarefaExistente = new TaskModel { Id = 1, Titulo = "Tarefa Original", Descricao = "Descricao Original" };
            context.Tasks.Add(tarefaExistente);
            await context.SaveChangesAsync();

            var handler = new DeletarTarefaCommandHandler(context);
            var command = new DeletarTarefaCommand { Id = 1 };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
            var tarefaDeletada = await context.Tasks.FindAsync(1);
            Assert.Null(tarefaDeletada);
        }

        [Fact]
        public async Task DeletarTarefaInexistente_RetornarUnit()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GerenciadorDbContext>()
                .UseInMemoryDatabase(databaseName: "GerenciadorDb")
                .Options;

            await using var context = new GerenciadorDbContext(options);
            var handler = new DeletarTarefaCommandHandler(context);
            var command = new DeletarTarefaCommand { Id = 99 };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
        }
    }
}
