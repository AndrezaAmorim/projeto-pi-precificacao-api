using ProjetoPiPrecificacao.Models;

namespace ProjetoPiPrecificacao.Repository.Interface
{
    public interface IProdutoRespository
    {
        bool Cadastrar(ProdutoModel model);
        ProdutoModel BuscarProdutoPorSku(string SKU);
    }
}
