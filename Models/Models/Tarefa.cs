using System.ComponentModel.DataAnnotations;

namespace Models.Models
{
    public class Tarefa
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Descricao { get; set; }
        
        public DateTime? DataInicioTarefa { get; set; }

        public DateTime? DataFinalTarefa { get; set; }

        [Required]
        public DateTime DataCriada { get; set; }

        [Required]
        public bool Ativo { get; set; }
    }
}
