using ProjetoPiPrecificacao.Models;
using ProjetoPiPrecificacao.Repository.Interface;

namespace ProjetoPiPrecificacao.Repository
{
    public class ProdutoRespository : IProdutoRespository
    {
        public bool Cadastrar(ProdutoModel model)
        {
            // Implementar a lógica de cadastro do produto aqui
            return true; // Retornar true se o cadastro for bem-sucedido, caso contrário, false
        }

        public ProdutoModel BuscarProdutoPorSku(string SKU)
        {
            // Implementar a lógica de busca do produto por SKU aqui
            return new ProdutoModel(); // Retornar o produto encontrado ou null se não encontrado
        }
    }
}
