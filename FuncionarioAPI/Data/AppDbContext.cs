using Microsoft.EntityFrameworkCore;
using FuncionarioAPI.Models;

namespace FuncionarioAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Funcionario> Funcionarios { get; set; }
    }
}