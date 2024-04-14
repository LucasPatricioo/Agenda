using System.ComponentModel.DataAnnotations;

namespace Models.Models
{
    public class Usuario
    {

        [Key]
        public int Id { get; set; }

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
        public DateTime DataCriada { get; set; }

        [Required]
        public bool Ativo { get; set; }
    }
}
