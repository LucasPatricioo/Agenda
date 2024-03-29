using AutoMapper;
using Models.DTO;
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
