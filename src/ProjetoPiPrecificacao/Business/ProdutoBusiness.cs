using ProjetoPiPrecificacao.Business.Interface;
using ProjetoPiPrecificacao.Models;
using ProjetoPiPrecificacao.Repository.Interface;

namespace ProjetoPiPrecificacao.Business
{
    public class ProdutoBusiness : IProdutoBusiness
    {
        private readonly IProdutoRepository _produtoRepository;
        public ProdutoBusiness(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public bool Cadastrar(ProdutoModel model)
        {
            return _produtoRepository.Cadastrar(model);
        }

        public ProdutoModel BuscarProdutoPorSku(string SKU)
        {
            return _produtoRepository.BuscarProdutoPorSku(SKU);
        }
    }
}
