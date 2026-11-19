using System.ComponentModel.DataAnnotations;

namespace Amparo.Api.Models;

public class CidadaoEntrada
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(150, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 150 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
    public DateTime? DataNascimento { get; set; }

    [Required(ErrorMessage = "O telefone é obrigatório.")]
    [StringLength(20, MinimumLength = 8, ErrorMessage = "O telefone deve ter entre 8 e 20 caracteres.")]
    public string Telefone { get; set; } = string.Empty;

    [Required(ErrorMessage = "O endereço é obrigatório.")]
    [StringLength(300, MinimumLength = 5, ErrorMessage = "O endereço deve ter entre 5 e 300 caracteres.")]
    public string Endereco { get; set; } = string.Empty;

    [StringLength(300, ErrorMessage = "A condição deve ter no máximo 300 caracteres.")]
    public string? Condicao { get; set; }

    public bool Ativo { get; set; } = true;
}
