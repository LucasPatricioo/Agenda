using System.ComponentModel.DataAnnotations;

namespace Models.DTO.Usuario
{
    public class NovoUsuario
    {
        [Required]
        [StringLength(255)]
        public string Nome { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Login { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required]
        public string Salt { get; set; }

        [Required]
        public DateTime DataCriada { get; set; } = DateTime.Now;

        [Required]
        public bool Ativo { get; set; } = true;
    }
}
