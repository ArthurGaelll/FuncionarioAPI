using System.ComponentModel.DataAnnotations;

namespace FuncionarioAPI.Models
{
    public class Funcionario
    {
        [Key] // Isso define que o ID será a Chave Primária automática
        public int Id { get; set; }

        [Required] // Define que o campo é obrigatório no banco
        public string Nome { get; set; } = string.Empty;

        public string Cargo { get; set; } = string.Empty;

        public decimal Salario { get; set; }
    }
}