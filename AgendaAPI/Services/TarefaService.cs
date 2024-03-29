using AgendaAPI.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Models.DTO;
using Models.Models;
using System.Data.SqlTypes;

namespace AgendaAPI.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly IMapper _mapper;
        private readonly ITarefaContext _tarefaContext;
        public TarefaService(IMapper mapper, ITarefaContext tarefaContext)
        {
            _mapper = mapper;
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

            return _mapper.Map<Tarefa>(tarefaRegistrada);
        }

        public Tarefa BuscarTarefa(BuscarTarefa buscarTarefa)
        {
            Tarefa tarefaRecuperada = _tarefaContext.BuscarTarefa(buscarTarefa);
            if (tarefaRecuperada == null)
                throw new SqlNullValueException();

            return _mapper.Map<Tarefa>(tarefaRecuperada);
        }
    }
}
