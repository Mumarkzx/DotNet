using Amparo.Api.Data;
using Amparo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Amparo.Api.Controllers;

[ApiController]
[Route("api/cidadaos")]
public class CidadaosController(AmparoDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cidadao>>> GetTodos() =>
        Ok(await context.Cidadaos.AsNoTracking().OrderBy(item => item.Id).ToListAsync());

    [HttpGet("{id:long}")]
    public async Task<ActionResult<Cidadao>> GetPorId(long id)
    {
        var cidadao = await context.Cidadaos.AsNoTracking().FirstOrDefaultAsync(item => item.Id == id);
        return cidadao is null
            ? NotFound(new { mensagem = $"Cidadão com ID {id} não encontrado." })
            : Ok(cidadao);
    }

    [HttpPost]
    public async Task<ActionResult<Cidadao>> Criar(CidadaoEntrada entrada)
    {
        var cidadao = new Cidadao
        {
            Nome = entrada.Nome,
            DataNascimento = entrada.DataNascimento!.Value,
            Telefone = entrada.Telefone,
            Endereco = entrada.Endereco,
            Condicao = entrada.Condicao,
            Ativo = entrada.Ativo
        };

        context.Cidadaos.Add(cidadao);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPorId), new { id = cidadao.Id }, cidadao);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Atualizar(long id, CidadaoEntrada entrada)
    {
        var cidadao = await context.Cidadaos.FindAsync(id);
        if (cidadao is null)
            return NotFound(new { mensagem = $"Cidadão com ID {id} não encontrado." });

        cidadao.Nome = entrada.Nome;
        cidadao.DataNascimento = entrada.DataNascimento!.Value;
        cidadao.Telefone = entrada.Telefone;
        cidadao.Endereco = entrada.Endereco;
        cidadao.Condicao = entrada.Condicao;
        cidadao.Ativo = entrada.Ativo;
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Excluir(long id)
    {
        var cidadao = await context.Cidadaos.FindAsync(id);
        if (cidadao is null)
            return NotFound(new { mensagem = $"Cidadão com ID {id} não encontrado." });

        if (await context.Cuidadores.CountAsync(item => item.CidadaoId == id) > 0)
            return Conflict(new { mensagem = "O cidadão não pode ser excluído enquanto possuir cuidadores vinculados." });

        context.Cidadaos.Remove(cidadao);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
