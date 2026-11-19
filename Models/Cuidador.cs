using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Amparo.Api.Models;

public class Cuidador
{
    public long Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(150, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 150 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O telefone é obrigatório.")]
    [StringLength(20, MinimumLength = 8, ErrorMessage = "O telefone deve ter entre 8 e 20 caracteres.")]
    public string Telefone { get; set; } = string.Empty;

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
    [StringLength(150, ErrorMessage = "O e-mail deve ter no máximo 150 caracteres.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "O parentesco é obrigatório.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "O parentesco deve ter entre 2 e 50 caracteres.")]
    public string Parentesco { get; set; } = string.Empty;

    [Range(1, 99, ErrorMessage = "A prioridade deve estar entre 1 e 99.")]
    public int Prioridade { get; set; }

    [Range(1, long.MaxValue, ErrorMessage = "Informe um cidadão válido.")]
    public long CidadaoId { get; set; }

    [JsonIgnore]
    public Cidadao? Cidadao { get; set; }
}
