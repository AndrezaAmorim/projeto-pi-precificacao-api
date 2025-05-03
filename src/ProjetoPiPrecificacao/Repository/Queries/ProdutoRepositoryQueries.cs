namespace ProjetoPiPrecificacao.Repository.Queries
{
    public class ProdutoRepositoryQueries
    {
        public readonly string Cadastrar;
        public readonly string BuscarProdutoPorSku;

        public ProdutoRepositoryQueries()
        {
            Cadastrar = CadastrarQuery();
            BuscarProdutoPorSku = BuscarProdutoPorSkuQuery();
        }

        private string CadastrarQuery()
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
