using System.ComponentModel.DataAnnotations;

namespace Models.DTO.Tarefa
{
    public class AlterarTarefa
    {
        [Key]
        public int Id { get; set; }

        public DateTime? DataInicioTarefa { get; set; }

        public DateTime? DataFinalTarefa { get; set; }

        [Required]
        public bool Ativo { get; set; }

    }
}
