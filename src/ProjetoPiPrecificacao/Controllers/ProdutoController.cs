using Microsoft.AspNetCore.Mvc;
using ProjetoPiPrecificacao.Business.Interface;
using ProjetoPiPrecificacao.Models;

namespace ProjetoPiPrecificacao.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoBusiness _produtoBusiness;

        public ProdutoController(IProdutoBusiness produtoBusiness)
        {
            _produtoBusiness = produtoBusiness;
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] ProdutoModel model)
        {

            bool retorno = _produtoBusiness.Cadastrar(model);

            if (!retorno)
                return StatusCode(500);

            return Ok(new
            {
                sucesso = true,
                mensagem = "Cadastro realizado com sucesso!"
            });
        }

        [HttpGet]
        public IActionResult BuscarProdutoPorSku([FromQuery] string SKU)
        {
            ProdutoModel? retorno = _produtoBusiness.BuscarProdutoPorSku(SKU);

            return Ok(retorno);
        }
    }
}
