using Models.DTO;
using Models.Models;

namespace AgendaAPI.Interfaces
{
    public interface ITarefaService
    {
        public Tarefa RegistrarNovaTarefa(NovaTarefa novaTarefa);
        public Tarefa BuscarTarefa(BuscarTarefa buscarTarefa);
    }
}
