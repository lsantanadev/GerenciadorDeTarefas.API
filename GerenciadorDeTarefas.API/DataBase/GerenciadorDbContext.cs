using GerenciadorDeTarefas.API.Entities;
using GerenciadorDeTarefas.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeTarefas.API.DataBase
{
    public class GerenciadorDbContext : DbContext
        {           
            public DbSet<TaskModel> Tasks { get; set; }
            public DbSet<User> Users { get; set; }

        public GerenciadorDbContext(DbContextOptions<GerenciadorDbContext> options)
         : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("DataSource=banco.db;Cache=Shared");
            }
        }
    }
}
