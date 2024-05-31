using System.Threading;
using System.Threading.Tasks;
using GerenciadorDeTarefas.API.Commands;
using GerenciadorDeTarefas.API.DataBase;
using GerenciadorDeTarefas.API.Entities;
using GerenciadorDeTarefas.API.Services;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Xunit;

namespace Gerenciador.UnitTests.CommandsTesUnit
{
    public class CriarUsuarioCommandHandlerTests
    {
        [Fact]
        public async Task CriarUsuario_ComDadosCorretos_RetornarIdDoUsuario()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GerenciadorDbContext>()
                .UseInMemoryDatabase(databaseName: "GerenciadorDb")
                .Options;

            var authService = Substitute.For<IAuthService>(); // Mock do serviço de autenticação
            var dbContext = new GerenciadorDbContext(options);
            var handler = new CriarUsuarioCommandHandler(dbContext, authService);

            // Act
            var command = new CriarUsuarioCommand
            {
                FullName = "Lara Santana",
                Email = "lsdev.com",
                Password = "senhasegura",
                Role = "Gerente"
            };

            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            var user = await dbContext.Users.FindAsync(result);
            Assert.NotNull(user);
            Assert.Equal("Lara Santana", user.FullName);
            Assert.Equal("lsdev.com", user.Email);
            Assert.Equal("Gerente", user.Role);
        }
    }
}
