using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato do e-mail é inválido.")]
        [StringLength(100, ErrorMessage = "E-mail deve ter no máximo {1} caracteres.")]
        public string Email { get; set; }
    }
}
