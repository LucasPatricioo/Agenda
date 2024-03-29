using System.ComponentModel.DataAnnotations;

namespace Models.DTO
{
    public class NovaTarefa
    {
        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(255)]
        public string Descricao { get; set; }

        [Required]
        public DateTime DataCriada { get; set; }

        [Required]
        public bool Ativo { get; set; }
    }
}
