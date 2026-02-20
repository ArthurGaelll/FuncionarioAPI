using FuncionarioAPI.Data;
using FuncionarioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SqlKata.Execution;
using PetaPoco; // Adicionado para reconhecer o IDatabase

namespace FuncionarioAPI.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context; // Entity Framework
        private readonly QueryFactory _db;      // SQLKata
        private readonly IDatabase _peta;       // PetaPoco

        // O Construtor agora recebe as três ferramentas
        public EmployeeController(AppDbContext context, QueryFactory db, IDatabase peta)
        {
            _context = context;
            _db = db;
            _peta = peta;
        }

        // --- MÉTODOS COM SQLKATA ---

        [HttpGet]
        public async Task<IActionResult> GetFuncionarios()
        {
            var funcionarios = await _db.Query("Funcionarios").GetAsync<Funcionario>();
            return Ok(funcionarios);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string? pais)
        {
            var query = _db.Query("Funcionarios");
            if (!string.IsNullOrEmpty(pais))
            {
                query.Where("Pais", "like", $"%{pais}%");
            }
            var resultado = await query.GetAsync<Funcionario>();
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Funcionario f)
        {
            await _db.Query("Funcionarios").InsertAsync(new
            {
                Nome = f.Nome,
                Idade = f.Idade,
                Pais = f.Pais,
                Trabalho = f.Trabalho,
                SalarioAnual = f.SalarioAnual
            });
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Funcionario f)
        {
            await _db.Query("Funcionarios")
                     .Where("Id", id)
                     .UpdateAsync(new
                     {
                         Nome = f.Nome,
                         SalarioAnual = f.SalarioAnual
                     });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _db.Query("Funcionarios")
                     .Where("Id", id)
                     .DeleteAsync();
            return NoContent();
        }

        // --- MÉTODOS COM PETAPOCO (Novos) ---

        // Exemplo: Buscar todos usando SQL Puro com PetaPoco
        [HttpGet("peta-list")]
        public IActionResult GetPetaList()
        {
            var sql = "SELECT * FROM Funcionarios";
            var resultado = _peta.Fetch<Funcionario>(sql);
            return Ok(resultado);
        }

        // Exemplo: Contar total de registros (Scalar query)
        [HttpGet("total")]
        public IActionResult GetTotal()
        {
            var total = _peta.ExecuteScalar<int>("SELECT COUNT(*) FROM Funcionarios");
            return Ok(new { total_funcionarios = total });
        }
    }
}