namespace ProjetoPiPrecificacao.Repository.Queries
{
    public class PrecificacaoRepositoryQueries
    {
        public readonly string Salvar;

        public PrecificacaoRepositoryQueries()
        {
            Salvar = SalvarQuery();
        }

        private string SalvarQuery()
        {
            return $@"
                INSERT INTO public.""dbo.Tabela_Preco_Produto""
                (""Id_Produto"", ""Preco_Sugerido_STSP"", ""Preco_Venda"", ""Desconto"",
                 ""Preco_Com_Desconto"", ""Margem_Liquida"", ""Margem_Bruta"", ""Valor_Lucro"",
                 ""Data_Alteracao_Preco)
                VALUES (@IdProduto, @PrecoSugeridoSTSP, @PrecoVenda, @Desconto, @PrecoDesconto,
                 @MargemLiquida, @MargemBruta, @Lucro, @DataAlteracaoPreco)
            ";
        }
    }
}
