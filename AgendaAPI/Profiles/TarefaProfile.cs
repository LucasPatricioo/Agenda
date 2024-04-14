using AutoMapper;
using Models.DTO.Tarefa;
using Models.Models;

namespace AgendaAPI.Profiles
{
    public class TarefaProfile : Profile
    {
        public TarefaProfile()
        {
            CreateMap<NovaTarefa, Tarefa>();
        }
    }
}
