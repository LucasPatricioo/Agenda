using Microsoft.AspNetCore.Mvc;
using Models.DTO;

namespace AgendaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        [HttpPost]
        public IActionResult NovaTarefa([FromBody] NovaTarefa tarefaRecebida)
        {
            if (tarefaRecebida == null)
                return BadRequest();



            return Ok(tarefaRecebida);
        }

        [HttpGet]
        public dynamic BuscarTarefa()
        {
            return Ok();
        }
    }
}
