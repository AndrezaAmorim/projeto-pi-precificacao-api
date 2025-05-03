using ProjetoPiPrecificacao.Business.Interface;
using ProjetoPiPrecificacao.Models;
using ProjetoPiPrecificacao.Repository.Interface;

namespace ProjetoPiPrecificacao.Business
{
    public class ProdutoBusiness : IProdutoBusiness
    {
        private readonly IProdutoRespository _produtoRespository;
        public ProdutoBusiness(IProdutoRespository produtoRespository)
        {
            _produtoRespository = produtoRespository;
        }

        public bool Cadastrar(ProdutoModel model)
        {
            return _produtoRespository.Cadastrar(model);
        }

        public ProdutoModel BuscarProdutoPorSku(string SKU)
        {
            return _produtoRespository.BuscarProdutoPorSku(SKU);
        }
    }
}
