using GerenciadorDeTarefas.API.DataBase;
using GerenciadorDeTarefas.API.Models;
using GerenciadorDeTarefas.API.Queries.ObterTodasTarefas;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Gerenciador.UnitTests.QueriesTestUnit
{
    public class ObterTodasTarefasQueryHandlerTests
    {
        [Fact]
        public async Task TresTarefasExistem_RetornarTresTarefas()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GerenciadorDbContext>()
                .UseInMemoryDatabase(databaseName: "GerenciadorDb")
                .Options;

            await using var context = new GerenciadorDbContext(options);
            context.Tasks.AddRange(new List<TaskModel>
    {
        new TaskModel { Id = 10, Titulo = "Tarefa 1", Descricao = "Descricao 1" },
        new TaskModel { Id = 20, Titulo = "Tarefa 2", Descricao = "Descricao 2" },
        new TaskModel { Id = 30, Titulo = "Tarefa 3", Descricao = "Descricao 3" }
    });
            await context.SaveChangesAsync(); // Salvar as alterações para gerar IDs únicos para as tarefas

            var handler = new ObterTodasTarefasQueryHandler(context);

            // Act
            var result = await handler.Handle(new ObterTodasTarefasQuery(), CancellationToken.None);

            // Assert
            var taskList = Assert.IsType<List<TaskModel>>(result);
            Assert.Equal(3, taskList.Count);
            Assert.Equal("Tarefa 1", taskList[0].Titulo);
            Assert.Equal("Tarefa 2", taskList[1].Titulo);
            Assert.Equal("Tarefa 3", taskList[2].Titulo);
        }
    }
}
