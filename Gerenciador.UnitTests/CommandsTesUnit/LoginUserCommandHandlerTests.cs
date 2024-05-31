using System.Threading;
using System.Threading.Tasks;
using GerenciadorDeTarefas.API.Commands.LoginUsuario;
using GerenciadorDeTarefas.API.Entities;
using GerenciadorDeTarefas.API.Repositories;
using GerenciadorDeTarefas.API.Services;
using GerenciadorDeTarefas.API.ViewModels;
using NSubstitute;
using Xunit;

namespace Gerenciador.UnitTests.CommandsTestUnit
{
    public class LoginUserCommandHandlerTests
    {
        [Fact]
        public async Task LoginUsuario_UsuarioExiste_RetornaLoginUserViewModel()
        {
            // Arrange
            var email = "lsdev.com";
            var password = "senhasegura";
            var role = "gerente";

            var user = new User("Lara", email, "hashedpassword", role);

            var authService = Substitute.For<IAuthService>();
            authService.ComputeSha256Hash(password).Returns("hashedpassword");
            authService.GenerateJwtToken(email, role).Returns("generatedtoken");

            var userRepository = Substitute.For<IUserRepository>();
            userRepository.GetUserByEmailAndPasswordAsync(email, "hashedpassword").Returns(user);

            var handler = new LoginUserCommandHandler(authService, userRepository);
            var command = new LoginUsuarioCommand { Email = email, Password = password };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<LoginUserViewModel>(result);
            Assert.Equal(email, result.Email);
            Assert.Equal("generatedtoken", result.Token);
        }

        [Fact]
        public async Task LoginUsuario_UsuarioNaoExiste_RetornaNulo()
        {
            // Arrange
            var email = "naoexistente.com";
            var password = "naoexiste";

            var authService = Substitute.For<IAuthService>();
            authService.ComputeSha256Hash(password).Returns("hashedpassword");

            var userRepository = Substitute.For<IUserRepository>();
            userRepository.GetUserByEmailAndPasswordAsync(email, "hashedpassword").Returns((User)null);

            var handler = new LoginUserCommandHandler(authService, userRepository);
            var command = new LoginUsuarioCommand { Email = email, Password = password };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
