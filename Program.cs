using Amparo.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Oracle");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "A conexão Oracle não foi configurada. Use User Secrets ou a variável ConnectionStrings__Oracle.");
}

builder.Services.AddDbContext<AmparoDbContext>(options =>
    options.UseOracle(connectionString));
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var erros = context.ModelState
                .Where(item => item.Value?.Errors.Count > 0)
                .ToDictionary(
                    item => item.Key,
                    item => item.Value!.Errors.Select(erro =>
                        string.IsNullOrWhiteSpace(erro.ErrorMessage)
                            ? "Valor inválido."
                            : erro.ErrorMessage));

            return new BadRequestObjectResult(new
            {
                mensagem = "Um ou mais dados são inválidos.",
                erros
            });
        };
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();

public partial class Program;
