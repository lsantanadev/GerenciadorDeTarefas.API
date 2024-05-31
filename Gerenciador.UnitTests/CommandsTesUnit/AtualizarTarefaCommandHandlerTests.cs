using GerenciadorDeTarefas.API.Commands.AtualizarTarefa;
using GerenciadorDeTarefas.API.DataBase;
using GerenciadorDeTarefas.API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Gerenciador.UnitTests.CommandsTesUnit
{
    public class AtualizarTarefaCommandHandlerTests
    {
        [Fact]
        public async Task AtualizarTarefaExistente_RetornarUnit()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GerenciadorDbContext>()
                .UseInMemoryDatabase(databaseName: "GerenciadorDb")
                .Options;

            await using var context = new GerenciadorDbContext(options);
            var tarefaExistente = new TaskModel { Id = 1, Titulo = "Tarefa Original", Descricao = "Descricao Original" };
            context.Tasks.Add(tarefaExistente);
            await context.SaveChangesAsync();

            var handler = new AtualizarTarefaCommandHandler(context);
            var command = new AtualizarTarefaCommand { Id = 1, Titulo = "Tarefa Atualizada", Descricao = "Descricao Atualizada" };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
            var tarefaAtualizada = await context.Tasks.FindAsync(1);
            Assert.Equal("Tarefa Atualizada", tarefaAtualizada.Titulo);
            Assert.Equal("Descricao Atualizada", tarefaAtualizada.Descricao);
        }

        [Fact]
        public async Task AtualizarTarefaInexistente_ThrowException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GerenciadorDbContext>()
                .UseInMemoryDatabase(databaseName: "GerenciadorDb")
                .Options;

            await using var context = new GerenciadorDbContext(options);
            var handler = new AtualizarTarefaCommandHandler(context);
            var command = new AtualizarTarefaCommand { Id = 99, Titulo = "Tarefa Atualizada", Descricao = "Descricao Atualizada" };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
