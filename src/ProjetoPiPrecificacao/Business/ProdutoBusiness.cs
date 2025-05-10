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
            try 
            {
                int idProduto = _produtoRepository.CadastrarProduto(model);

                if (idProduto == 0)
                    throw new Exception("Erro ao cadastrar produto");

                model.IdProduto = idProduto;
                bool retorno = _produtoRepository.CadastrarCustoProduto(model);

                if (!retorno)
                    throw new Exception("Erro ao cadastrar custo do produto");

                return retorno;

            } catch (Exception ex) 
            {
                throw new Exception($"Erro ao cadastrar produto. {ex.Message}");
            }
        }

        public ProdutoModel? BuscarProdutoPorSku(string SKU)
        {
            return _produtoRepository.BuscarProdutoPorSku(SKU);
        }
    }
}
