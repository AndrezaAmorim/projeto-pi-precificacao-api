using Microsoft.AspNetCore.Mvc;

namespace ProjetoPiPrecificacao.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PrecificacaoController : ControllerBase
    {
        [HttpPost(Name = "Salvar")]
        public IActionResult Salvar()
        {
            return Ok("Teste realizado com sucesso!");
        }
    }
}

