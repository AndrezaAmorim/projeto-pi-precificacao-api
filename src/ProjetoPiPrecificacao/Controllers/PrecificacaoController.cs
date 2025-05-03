using Microsoft.AspNetCore.Mvc;
using ProjetoPiPrecificacao.Models;

namespace ProjetoPiPrecificacao.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class PrecificacaoController : ControllerBase
    {
        [HttpPost]
        public IActionResult Salvar([FromBody] PrecificacaoModel model)
        {
            if (model == null)
                return StatusCode(500);

            return Ok(new
            {
                sucesso = true,
                mensagem = "Teste realizado com sucesso!"
            });
        }

        [HttpPost]
        public IActionResult CalcularPreco([FromBody] PrecificacaoModel model)
        {
            return Ok(model);
        }
    }
}

