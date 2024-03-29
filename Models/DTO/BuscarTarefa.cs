using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class BuscarTarefa
    {
        public int Id { get; set; } = 0;
        public DateTime DataCriada { get; set; } = DateTime.MinValue;
    }
}
