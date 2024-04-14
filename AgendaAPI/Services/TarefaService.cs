using AgendaAPI.Interfaces;
using AutoMapper;
using Models.DTO.Tarefa;
using Models.Models;
using System.Data.SqlTypes;

namespace AgendaAPI.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaContext _tarefaContext;
        public TarefaService(ITarefaContext tarefaContext)
        {
            _tarefaContext = tarefaContext;
        }

        public Tarefa RegistrarNovaTarefa(NovaTarefa novaTarefa)
        {
            DateTime dataGerada = _tarefaContext.RegistrarNovaTarefa(novaTarefa);
            if (dataGerada == DateTime.MinValue)
                throw new SqlNullValueException();

            BuscarTarefa buscarTarefa = new BuscarTarefa() { DataCriada = dataGerada };

            Tarefa tarefaRegistrada = _tarefaContext.BuscarTarefa(buscarTarefa);
            if(tarefaRegistrada == null)
                throw new SqlNullValueException();

            return tarefaRegistrada;
        }

        public Tarefa BuscarTarefa(BuscarTarefa buscarTarefa)
        {
            Tarefa tarefaRecuperada = _tarefaContext.BuscarTarefa(buscarTarefa);
            if (tarefaRecuperada == null)
                throw new SqlNullValueException();

            return tarefaRecuperada;
        }

        public void AlterarEstadoTarefa(AlterarTarefa alterarTarefa)
        {
            _tarefaContext.AlterarEstadoTarefa(alterarTarefa);
        }
    }
}
