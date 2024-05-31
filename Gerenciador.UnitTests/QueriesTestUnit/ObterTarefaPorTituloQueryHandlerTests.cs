using GerenciadorDeTarefas.API.DataBase;
using GerenciadorDeTarefas.API.Queries.ObterTarefaPorTitulo;
using GerenciadorDeTarefas.API.ViewModels;
using Microsoft.EntityFrameworkCore;
using Xunit;
using GerenciadorDeTarefas.API.Models;

namespace Gerenciador.UnitTests.QueriesTestUnit
{
    public class ObterTarefaPorTituloQueryHandlerTests
    {
        [Fact]
        public async Task ObterTarefaPorTitulo_TituloExistente_RetornaListaDeTarefas()
        {
            // Arrange
            var titulo = "Tarefa teste";
            var options = new DbContextOptionsBuilder<GerenciadorDbContext>()
                .UseInMemoryDatabase(databaseName: "ObterTarefaPorTitulo_TituloExistente_RetornaListaDeTarefas")
                .Options;

            using (var context = new GerenciadorDbContext(options))
            {
                context.Tasks.AddRange(new List<TaskModel>
                {
                    new TaskModel { Id = 1, Titulo = "Tarefa teste", Descricao = "Descrição da Tarefa " },
                    new TaskModel { Id = 2, Titulo = "Tarefa 2", Descricao = "Descrição da tarefa" },
                    new TaskModel { Id = 3, Titulo = "Outra tarefa", Descricao = "Descrição da outra tarefa" }
                });
                await context.SaveChangesAsync();
            }

            using (var context = new GerenciadorDbContext(options))
            {
                var handler = new ObterTarefaPorTituloQueryHandler(context);
                var query = new ObterTarefaPorTituloQuery("Tarefa teste"); 

                // Act
                var result = await handler.Handle(query, CancellationToken.None);

                // Assert
                Assert.NotNull(result);
                Assert.IsType<List<TaskViewModel>>(result);
                Assert.Equal(1, result.Count); 
                Assert.Equal("Tarefa teste", result[0].Titulo); 
            }
        }

        [Fact]
        public async Task ObterTarefaPorTitulo_TituloInexistente_RetornaListaVazia()
        {
            // Arrange
            var titulo = "Inexistente";
            var options = new DbContextOptionsBuilder<GerenciadorDbContext>()
                .UseInMemoryDatabase(databaseName: "ObterTarefaPorTitulo_TituloInexistente_RetornaListaVazia")
                .Options;

            using (var context = new GerenciadorDbContext(options))
            {
                context.Tasks.AddRange(new List<TaskModel>
                {
                    new TaskModel { Id = 1, Titulo = "Tarefa 1", Descricao = "Descrição da tarefa 1" },
                    new TaskModel { Id = 2, Titulo = "Tarefa 2", Descricao = "Descrição da tarefa 2" },
                    new TaskModel { Id = 3, Titulo = "Outra tarefa", Descricao = "Descrição da outra tarefa" }
                });
                await context.SaveChangesAsync();
            }

            using (var context = new GerenciadorDbContext(options))
            {
                var handler = new ObterTarefaPorTituloQueryHandler(context);
                var query = new ObterTarefaPorTituloQuery(titulo);

                // Act
                var result = await handler.Handle(query, CancellationToken.None);

                // Assert
                Assert.NotNull(result);
                Assert.Empty(result); 
            }
        }
    }
}
