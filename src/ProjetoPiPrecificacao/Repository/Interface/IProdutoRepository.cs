using ProjetoPiPrecificacao.Models;

namespace ProjetoPiPrecificacao.Repository.Interface
{
    public interface IProdutoRepository
    {
        int CadastrarProduto(ProdutoModel model);
        bool CadastrarCustoProduto(ProdutoModel model);
        PrecificacaoModel? BuscarProdutoPorSku(string SKU);
    }
}
