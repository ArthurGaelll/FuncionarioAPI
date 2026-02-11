using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FuncionarioAPI.Data;
using FuncionarioAPI.Models;

namespace FuncionarioAPI.Controllers
{
    // Essas etiquetas (atributos) dizem que esta classe é uma API 
    // e que o endereço dela será "api/Funcionarios"
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Aqui o "Gerente" recebe a chave do banco de dados (o Contexto) 
        // para conseguir trabalhar.
        public FuncionariosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Funcionarios (O comando de "Listar")
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionarios()
        {
            // Ele vai na tabela de Funcionarios e transforma tudo em uma lista.
            return await _context.Funcionarios.ToListAsync();
        }

        // POST: api/Funcionarios (O comando de "Cadastrar")
        [HttpPost]
        public async Task<ActionResult<Funcionario>> PostFuncionario(Funcionario funcionario)
        {
            // Ele adiciona o novo funcionário na fila do banco...
            _context.Funcionarios.Add(funcionario);
            // ...e aqui ele realmente salva no arquivo .db.
            await _context.SaveChangesAsync();

            // Retorna o código 201 (Sucesso e Criado).
            return CreatedAtAction(nameof(GetFuncionarios), new { id = funcionario.Id }, funcionario);
        }

        // PUT: api/Funcionarios/5 (O comando de "Editar")
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFuncionario(int id, Funcionario funcionarioAtualizado)
        {
            if (id != funcionarioAtualizado.Id) return BadRequest();

            // Isso aqui avisa ao banco: "Atualize esse registro específico"
            _context.Entry(funcionarioAtualizado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Funcionarios.Any(e => e.Id == id)) return NotFound();
                else throw;
            }
            return NoContent();
        }

        // DELETE: api/Funcionarios/5 (O comando de "Remover")
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuncionario(int id)
        {
            // 1. Procura o funcionário no banco pelo ID enviado.
            var funcionario = await _context.Funcionarios.FindAsync(id);

            // 2. Se não existir, avisa que não encontrou (404).
            if (funcionario == null) return NotFound();

            // 3. Se existir, manda remover e salva no banco.
            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();

            return NoContent(); // Sucesso.
        }
    }
}