using Microsoft.AspNetCore.Mvc;
using ProjetoPiPrecificacao.Business.Interface;
using ProjetoPiPrecificacao.Models;

namespace ProjetoPiPrecificacao.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class PrecificacaoController : ControllerBase
    {
        private readonly IPrecificacaoBusiness _precificacaoBusiness;
        public PrecificacaoController(IPrecificacaoBusiness precificacaoBusiness)
        {
            _precificacaoBusiness = precificacaoBusiness;
        }

        [HttpPost]
        public IActionResult Salvar([FromBody] PrecificacaoModel model)
        {
            bool retorno = _precificacaoBusiness.Salvar(model);

            if (!retorno)
                return StatusCode(500);

            return Ok(new
            {
                sucesso = true,
                mensagem = "Salvo com sucesso!"
            });
        }

        [HttpPost]
        public IActionResult CalcularPreco([FromBody] PrecificacaoModel model)
        {
            PrecificacaoModel retorno = _precificacaoBusiness.CalcularPreco(model);
            return Ok(retorno);
        }
    }
}

