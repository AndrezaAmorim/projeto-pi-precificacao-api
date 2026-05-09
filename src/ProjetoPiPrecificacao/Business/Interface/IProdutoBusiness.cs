using ProjetoPiPrecificacao.Models;

namespace ProjetoPiPrecificacao.Business.Interface
{
    public interface IProdutoBusiness
    {
        bool Cadastrar(ProdutoModel model);
        bool Excluir(ProdutoModel model);
        ProdutoModel? BuscarProdutoPorSku(string SKU);
        Task CadastrarProdutoExcel(IFormFile arquivo);
    }
}
