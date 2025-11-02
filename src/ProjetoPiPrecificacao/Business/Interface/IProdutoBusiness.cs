using ProjetoPiPrecificacao.Models;

namespace ProjetoPiPrecificacao.Business.Interface
{
    public interface IProdutoBusiness
    {
        bool Cadastrar(ProdutoModel model);
        ProdutoModel? BuscarProdutoPorSku(string SKU);
        Task CadastrarProdutoExcel(IFormFile arquivo);
    }
}
