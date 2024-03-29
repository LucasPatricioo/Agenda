using AgendaAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.Models;

namespace AgendaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;

        public TarefaController(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        [HttpPost]
        public IActionResult NovaTarefa([FromBody] NovaTarefa tarefaRecebida)
        {
            if (tarefaRecebida == null)
                return BadRequest();

            Tarefa tarefaRegistrada = _tarefaService.RegistrarNovaTarefa(tarefaRecebida);

            return CreatedAtAction(nameof(BuscarTarefa), new { Id = tarefaRegistrada.Id }, tarefaRegistrada);
        }

        [HttpGet]
        public IActionResult BuscarTarefa(int idRecebido, DateTime dataCriadaRecebido)
        {
            if (idRecebido == null && dataCriadaRecebido == null)
                return BadRequest();

            BuscarTarefa buscaTarefaRecebida = new BuscarTarefa() { Id = idRecebido, DataCriada = dataCriadaRecebido };

            Tarefa tarefaRecuperada = _tarefaService.BuscarTarefa(buscaTarefaRecebida);

            return Ok(tarefaRecuperada);
        }
    }
}
