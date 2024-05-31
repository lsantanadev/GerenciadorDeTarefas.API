using MediatR;


namespace GerenciadorDeTarefas.API.Commands
{
    public class CriarTarefaCommand : IRequest<int>
    {
        // Construtor vazio necessário para deserialização
        public CriarTarefaCommand() { }

        // Construtor com parâmetros para inicialização
        public CriarTarefaCommand(string titulo, string descricao)
        {
            Titulo = titulo;
            Descricao = descricao;
           
        }

        public string Titulo { get; set; }
        public string Descricao { get; set; }
       
    }
}
