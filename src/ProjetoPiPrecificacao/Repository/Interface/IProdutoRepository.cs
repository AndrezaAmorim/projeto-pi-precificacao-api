using ProjetoPiPrecificacao.Models;

namespace ProjetoPiPrecificacao.Repository.Interface
{
    public interface IProdutoRepository
    {
        bool Cadastrar(ProdutoModel model);
        ProdutoModel BuscarProdutoPorSku(string SKU);
    }
}
