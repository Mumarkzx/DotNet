using Amparo.Api.Data;
using Amparo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Amparo.Api.Controllers;

[ApiController]
[Route("api/cuidadores")]
public class CuidadoresController(AmparoDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cuidador>>> GetTodos() =>
        Ok(await context.Cuidadores.AsNoTracking().OrderBy(item => item.Id).ToListAsync());

    [HttpGet("{id:long}")]
    public async Task<ActionResult<Cuidador>> GetPorId(long id)
    {
        var cuidador = await context.Cuidadores.AsNoTracking().FirstOrDefaultAsync(item => item.Id == id);
        return cuidador is null
            ? NotFound(new { mensagem = $"Cuidador com ID {id} não encontrado." })
            : Ok(cuidador);
    }

    [HttpPost]
    public async Task<ActionResult<Cuidador>> Criar(Cuidador cuidador)
    {
        if (await context.Cidadaos.CountAsync(item => item.Id == cuidador.CidadaoId) == 0)
            return BadRequest(new { mensagem = $"O cidadão com ID {cuidador.CidadaoId} não existe." });

        cuidador.Id = 0;
        context.Cuidadores.Add(cuidador);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPorId), new { id = cuidador.Id }, cuidador);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Atualizar(long id, Cuidador entrada)
    {
        var cuidador = await context.Cuidadores.FindAsync(id);
        if (cuidador is null)
            return NotFound(new { mensagem = $"Cuidador com ID {id} não encontrado." });
        if (await context.Cidadaos.CountAsync(item => item.Id == entrada.CidadaoId) == 0)
            return BadRequest(new { mensagem = $"O cidadão com ID {entrada.CidadaoId} não existe." });

        cuidador.Nome = entrada.Nome;
        cuidador.Telefone = entrada.Telefone;
        cuidador.Email = entrada.Email;
        cuidador.Parentesco = entrada.Parentesco;
        cuidador.Prioridade = entrada.Prioridade;
        cuidador.CidadaoId = entrada.CidadaoId;
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Excluir(long id)
    {
        var cuidador = await context.Cuidadores.FindAsync(id);
        if (cuidador is null)
            return NotFound(new { mensagem = $"Cuidador com ID {id} não encontrado." });

        context.Cuidadores.Remove(cuidador);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
