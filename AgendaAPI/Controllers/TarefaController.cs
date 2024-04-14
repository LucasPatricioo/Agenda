using AgendaAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO.Tarefa;
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

        [Authorize]
        [HttpPost]
        public IActionResult NovaTarefa([FromBody] NovaTarefa tarefaRecebida)
        {
            if (tarefaRecebida == null)
                return BadRequest();

            Tarefa tarefaRegistrada = _tarefaService.RegistrarNovaTarefa(tarefaRecebida);

            return CreatedAtAction(nameof(BuscarTarefa), new { Id = tarefaRegistrada.Id }, tarefaRegistrada);
        }

        [Authorize]
        [HttpGet]
        public IActionResult BuscarTarefa(int idRecebido, DateTime dataCriadaRecebido)
        {
            if (idRecebido == 0 && dataCriadaRecebido == DateTime.MinValue)
                return BadRequest("Parametros recebidos inválidos");

            BuscarTarefa buscaTarefaRecebida = new BuscarTarefa() { Id = idRecebido, DataCriada = dataCriadaRecebido };

            Tarefa tarefaRecuperada = _tarefaService.BuscarTarefa(buscaTarefaRecebida);

            return Ok(tarefaRecuperada);
        }

        [Authorize]
        [HttpPatch]
        public IActionResult AlterarEstadoTarefa([FromBody] AlterarTarefa tarefaAlterar)
        {
            if (tarefaAlterar == null)
                return BadRequest();

            _tarefaService.AlterarEstadoTarefa(tarefaAlterar);

            return CreatedAtAction(nameof(BuscarTarefa), new { Id = tarefaAlterar.Id });
        }
    }
}
