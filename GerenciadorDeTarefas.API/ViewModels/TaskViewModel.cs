
namespace GerenciadorDeTarefas.API.ViewModels
{
    public class TaskViewModel
    {
        public TaskViewModel(int id, string titulo, string descricao)
        {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
           
        }

        public int Id { get; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
      
    }
}
