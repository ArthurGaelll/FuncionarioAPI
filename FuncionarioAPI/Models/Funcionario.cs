using System.ComponentModel.DataAnnotations;

namespace FuncionarioAPI.Models
{
    public class Funcionario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A idade é obrigatória.")]
        public int Idade { get; set; }

        [Required(ErrorMessage = "O país é obrigatório.")]
        public string Pais { get; set; } = string.Empty;

        [Required(ErrorMessage = "O trabalho é obrigatório.")]
        public string Trabalho { get; set; } = string.Empty;

        [Required(ErrorMessage = "O salário é obrigatório.")]
        public decimal SalarioAnual { get; set; }
    }
}