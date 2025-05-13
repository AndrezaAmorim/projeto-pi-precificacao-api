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
                INSERT INTO public.""dbo.Tabela_Produto""
                (""SKU"", ""Descricao_Produto"", ""Fornecedor"", ""Peso"", ""Altura"", ""Largura"", ""Kit"")      
                VALUES (@SKU, @NomeProduto, @Fornecedor, @Peso, @Altura, @Largura, @Kit)                    
                RETURNING ""Id_Produto"";
            ";
        }

        private string CadastrarCustoProdutoQuery()
        {
            return $@"
                INSERT INTO public.""dbo.Tabela_Custo_Produto""
                (""Id_Produto"", ""Data_Compra"", ""Tipo_Compra"", ""Preco_Unitario"", ""Custos_Extras"",
                ""ICMS"", ""IPI"", ""PISCOFINS"", ""MVAAjustado"", ""ICMSRetido"", ""ICMSProprio"")
                VALUES (@IdProduto, @DataCompra, @TipoCompra, @PrecoUnitario, @CustosExtras, @ICMS,
                 @IPI, @PisCofins, @MvaAjustado, @IcmsRetido, @IcmsProprio);
            ";
        }

        private string BuscarProdutoPorSkuQuery()
        {
            return $@"
                SELECT * FROM public.""dbo.Tabela_Produto"" dtp
                INNER JOIN public.""dbo.Tabela_Custo_Produto"" dtcp
                ON dtcp.""Id_Produto"" = dtp.""Id_Produto""
                LEFT JOIN public.""dbo.Tabela_Preco_Produto"" dtpp
                ON dtpp.""Id_Produto"" = dtp.""Id_Produto""
                WHERE dtp.""SKU"" = @SKU    
            ";
        }
    }
}
