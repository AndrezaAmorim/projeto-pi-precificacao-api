namespace ProjetoPiPrecificacao.Repository.Queries
{
    public class ProdutoRepositoryQueries
    {
        public readonly string CadastrarProduto;
        public readonly string CadastrarCustoProduto;
        public readonly string BuscarProdutoPorSku;

        public ProdutoRepositoryQueries()
        {
            CadastrarProduto = CadastrarProdutoQuery();
            CadastrarCustoProduto = CadastrarCustoProdutoQuery();
            BuscarProdutoPorSku = BuscarProdutoPorSkuQuery();
        }

        private string CadastrarProdutoQuery()
        {
            return $@"
                // adicionar o SELECT SCOPE_IDENTITY() para retornar o ID do produto inserido
            ";
        }

        private string CadastrarCustoProdutoQuery()
        {
            return $@"
               
            ";
        }

        private string BuscarProdutoPorSkuQuery()
        {
            return $@"
                
            ";
        }
    }
}
