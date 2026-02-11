// Essas linhas são como "import" no Python; elas trazem ferramentas prontas.
using Microsoft.EntityFrameworkCore; // Traz o motor que entende de bancos de dados.
using FuncionarioAPI.Models;       // Permite que o código use a sua classe 'Funcionario'.

namespace FuncionarioAPI.Data
{
    // O 'DbContext' é a ponte principal. 
    // Quando escrevemos ': DbContext', estamos dizendo que nossa classe AppDbContext 
    // vai herdar (copiar) todas as habilidades de um banco de dados profissional.
    public class AppDbContext : DbContext
    {
        // Este é o 'Construtor'. Ele é necessário para que o sistema (no Program.cs) 
        // passe as configurações, como o caminho do arquivo de banco, para cá.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // O 'DbSet' é como se fosse a representação da TABELA dentro do seu código.
        // Aqui você está dizendo: "Crie uma tabela chamada 'Funcionarios' 
        // baseada no modelo que defini na classe 'Funcionario'".
        public DbSet<Funcionario> Funcionarios { get; set; }
    }
}