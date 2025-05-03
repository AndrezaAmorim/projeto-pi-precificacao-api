using Microsoft.AspNetCore.Mvc;
using ProjetoPiPrecificacao.Models;

namespace ProjetoPiPrecificacao.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class ProdutoController : ControllerBase
    {
        [HttpPost]
        public IActionResult Cadastrar([FromBody] PrecificacaoModel model)
        {
            if (model == null)
                return StatusCode(500);

            return Ok(new
            {
                sucesso = true,
                mensagem = "Teste realizado com sucesso!"
            });
        }

        [HttpGet]
        public IActionResult BuscarProdutoPorSku([FromQuery] string SKU)
        {
            PrecificacaoModel retorno = new PrecificacaoModel
            {
                IdProduto = 1,
                SKU = SKU,
                NomeProduto = "Produto Teste",
                Fornecedor = "Fornecedor Teste",
                Peso = 1.0f,
                Altura = 10.0f,
                Largura = 5.0f,
                Kit = true,
                DataCompra = DateTime.Now,
                TipoCompra = "Teste",
                PrecoUnitario = 100.0f,
                CustosExtras = 10.0f,
                ICMS = 18.0f,
                IPI = 5.0f,
                PisCofins = 3.0f,
                MvaAjustado = 12.0f,
                IcmsRetido = 7.0f,
                IcmsProprio = 11.0f
            };

            return Ok(retorno);
        }
    }
}
