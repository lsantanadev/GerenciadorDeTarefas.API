namespace GerenciadorDeTarefas.API.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
    }
}

//Sobre o uso do public DateTime? DataConclusao { get; set; } no Front-End:
//Optei por não incluir a propriedade de (DataConclusao) no Front-End por enquanto.