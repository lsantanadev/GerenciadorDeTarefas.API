using System.Threading;
using System.Threading.Tasks;
using GerenciadorDeTarefas.API.Entities;
using GerenciadorDeTarefas.API.Queries.ObterUsuario;
using GerenciadorDeTarefas.API.Repositories;
using GerenciadorDeTarefas.API.ViewModels;
using NSubstitute;
using Xunit;

namespace Gerenciador.UnitTests.QueriesTestUnit
{
    public class ObterUsuarioQueryHandlerTests
    {
        [Fact]
        public async Task ObterUsuario_PorId_RetornaUsuarioViewModel()
        {
            // Arrange
            var userId = 1;
            var user = new User("Lara", "lsdev.com", "senhasegura", "gerente");

            var userRepository = Substitute.For<IUserRepository>();
            userRepository.GetByIdAsync(userId).Returns(user);

            var handler = new GetUserQueryHandler(userRepository);
            var query = new ObterUsuarioQuery(userId); 

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserViewModel>(result);
            Assert.Equal(user.FullName, result.FullName);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task ObterUsuario_PorId_Inexistente_RetornaNulo()
        {
            // Arrange
            var userId = 99;

            var userRepository = Substitute.For<IUserRepository>();
            userRepository.GetByIdAsync(userId).Returns((User)null);

            var handler = new GetUserQueryHandler(userRepository);
            var query = new ObterUsuarioQuery(userId); 

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
