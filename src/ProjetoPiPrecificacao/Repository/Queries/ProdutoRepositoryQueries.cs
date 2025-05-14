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
                SELECT 
                    dtp.""Id_Produto""              AS IdProduto,
                    dtp.""SKU""                     AS SKU,
                    dtp.""Descricao_Produto""       AS NomeProduto,
                    dtp.""Fornecedor""              AS Fornecedor,
                    dtp.""Peso""                    AS Peso,
                    dtp.""Altura""                  AS Altura,
                    dtp.""Largura""                 AS Largura,
                    dtp.""Kit""                     AS Kit,
                    dtcp.""Id_Custo_Produto""       AS IdCustoProduto,
                    dtcp.""Data_Compra""            AS DataCompra,
                    dtcp.""Tipo_Compra""            AS TipoCompra,
                    dtcp.""Preco_Unitario""         AS PrecoUnitario,
                    dtcp.""Custos_Extras""          AS CustosExtras,
                    dtcp.""ICMS""                   AS ICMS,
                    dtcp.""IPI""                    AS IPI,
                    dtcp.""PISCOFINS""              AS PisCofins,
                    dtcp.""MVAAjustado""            AS MvaAjustado,
                    dtcp.""ICMSRetido""             AS IcmsRetido,
                    dtcp.""ICMSProprio""            AS IcmsProprio,
                    dtpp.""Id_Preco_Produto""       AS IdPrecoProduto,
                    dtpp.""Preco_Sugerido_STSP""    AS PrecoSugeridoSTSP,
                    dtpp.""Preco_Venda""            AS PrecoVenda,
                    dtpp.""Desconto""               AS Desconto,
                    dtpp.""Preco_Com_Desconto""     AS PrecoDesconto,
                    dtpp.""Margem_Liquida""         AS MargemLiquida,
                    dtpp.""Margem_Bruta""           AS MargemBruta,
                    dtpp.""Valor_Lucro""            AS Lucro,
                    dtpp.""Data_Alteracao_Preco""   AS DataAlteracaoPreco
                FROM public.""dbo.Tabela_Produto"" dtp
                INNER JOIN public.""dbo.Tabela_Custo_Produto"" dtcp
                ON dtcp.""Id_Produto"" = dtp.""Id_Produto""
                LEFT JOIN public.""dbo.Tabela_Preco_Produto"" dtpp
                ON dtpp.""Id_Produto"" = dtp.""Id_Produto""
                WHERE dtp.""SKU"" = @SKU    
            ";
        }
    }
}
