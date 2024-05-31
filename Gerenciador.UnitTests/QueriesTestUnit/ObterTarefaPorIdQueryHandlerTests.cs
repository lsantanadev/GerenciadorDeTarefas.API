using GerenciadorDeTarefas.API.DataBase;
using GerenciadorDeTarefas.API.Models;
using GerenciadorDeTarefas.API.Queries;
using GerenciadorDeTarefas.API.Queries.ObterTarefaPorId;
using GerenciadorDeTarefas.API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Gerenciador.UnitTests.QueriesTestUnit
{
    public class ObterTarefaPorIdQueryHandlerTests
    {
        [Fact]
        public async Task ComIdDeTarefaExistente_RetornaTaskViewModel()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GerenciadorDbContext>()
                .UseInMemoryDatabase(databaseName: "ComIdDeTarefaExistente_RetornaTaskViewModel")
                .Options;

            using (var context = new GerenciadorDbContext(options))
            {
                context.Tasks.Add(new TaskModel { Id = 1, Titulo = "Tarefa 1", Descricao = "Descrição da tarefa 1" });
                await context.SaveChangesAsync();
            }

            using (var context = new GerenciadorDbContext(options))
            {
                var handler = new ObterTarefaPorIdQueryHandler(context);
                var query = new ObterTarefaPorIdQuery(1);

                // Act
                var result = await handler.Handle(query, CancellationToken.None);

                // Assert
                Assert.NotNull(result);
                Assert.IsType<TaskViewModel>(result);
                Assert.Equal(1, result.Id);
                Assert.Equal("Tarefa 1", result.Titulo);
                Assert.Equal("Descrição da tarefa 1", result.Descricao);
            }
        }

        [Fact]
        public async Task ComIdDeTarefaNaoExistente_RetornaNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GerenciadorDbContext>()
                .UseInMemoryDatabase(databaseName: "ComIdDeTarefaNaoExistente_RetornaNull")
                .Options;

            using (var context = new GerenciadorDbContext(options))
            {
                context.Tasks.Add(new TaskModel { Id = 1, Titulo = "Tarefa 1", Descricao = "Descrição da tarefa 1" });
                await context.SaveChangesAsync();
            }

            using (var context = new GerenciadorDbContext(options))
            {
                var handler = new ObterTarefaPorIdQueryHandler(context);
                var query = new ObterTarefaPorIdQuery(100); 

                // Act
                var result = await handler.Handle(query, CancellationToken.None);

                // Assert
                Assert.Null(result);
            }
        }
    }
}
