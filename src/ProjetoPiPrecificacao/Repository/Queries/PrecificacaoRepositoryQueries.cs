namespace ProjetoPiPrecificacao.Repository.Queries
{
    public class PrecificacaoRepositoryQueries
    {
        public readonly string Salvar;
        public readonly string CalcularPreco;

        public PrecificacaoRepositoryQueries()
        {
            Salvar = SalvarQuery();
            CalcularPreco = CalcularPrecoQuery();
        }

        private string SalvarQuery()
        {
            return $@"

            ";
        }

        private string CalcularPrecoQuery()
        {
            return $@"

            ";
        }
    }
}
