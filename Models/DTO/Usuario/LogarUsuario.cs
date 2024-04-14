using System.ComponentModel.DataAnnotations;

namespace Models.DTO.Usuario
{
    public class LogarUsuario
    {
        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}
