using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FuncionarioAPI.Data;
using FuncionarioAPI.Models;

namespace FuncionarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FuncionariosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Funcionarios (Lista todos)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionarios()
        {
            return await _context.Funcionarios.ToListAsync();
        }

        // POST: api/Funcionarios (Cadastra um novo)
        [HttpPost]
        public async Task<ActionResult<Funcionario>> PostFuncionario(Funcionario funcionario)
        {
            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFuncionarios), new { id = funcionario.Id }, funcionario);
        }
        // PUT: api/Funcionarios/5 (Atualizar um funcionário que já existe)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFuncionario(int id, Funcionario funcionarioAtualizado)
        {
            // Verificamos se o ID que você mandou na URL é o mesmo do corpo da mensagem
            if (id != funcionarioAtualizado.Id) return BadRequest();

            // Avisamos ao banco que esses dados foram modificados
            _context.Entry(funcionarioAtualizado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(); // Salva as mudanças no SQLite
            }
            catch (DbUpdateConcurrencyException)
            {
                // Se o ID não existir no banco, ele dá erro
                if (!_context.Funcionarios.Any(e => e.Id == id)) return NotFound();
                else throw;
            }

            return NoContent(); // Retorna "Vazio" (sucesso sem mensagem)
        }

        // DELETE: api/Funcionarios/5 (Remover um funcionário)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuncionario(int id)
        {
            // 1. Procuramos o funcionário pelo ID
            var funcionario = await _context.Funcionarios.FindAsync(id);

            // 2. Se não encontrar, avisa que não existe
            if (funcionario == null) return NotFound();

            // 3. Se encontrar, remove e salva
            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

