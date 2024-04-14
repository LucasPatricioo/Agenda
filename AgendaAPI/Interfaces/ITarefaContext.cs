using Models.DTO.Tarefa;
using Models.Models;

namespace AgendaAPI.Interfaces
{
    public interface ITarefaContext
    {
        public DateTime RegistrarNovaTarefa(NovaTarefa novaTarefa);
        public Tarefa BuscarTarefa(BuscarTarefa buscaTarefa);
        public void AlterarEstadoTarefa(AlterarTarefa alterarTarefa);
    }
}
