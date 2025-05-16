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
                MERGE INTO public.""dbo.Tabela_Produto"" AS Destino
                USING (
                    SELECT 
                        @SKU AS ""SKU"", 
                        @NomeProduto AS ""Descricao_Produto"", 
                        @Fornecedor AS ""Fornecedor"", 
                        @Peso AS ""Peso"", 
                        @Altura AS ""Altura"", 
                        @Largura AS ""Largura"", 
                        @Kit AS ""Kit""
                ) AS Origem
                ON Destino.""SKU"" = Origem.""SKU""
                WHEN MATCHED THEN
                    UPDATE SET
                        ""Descricao_Produto"" = Origem.""Descricao_Produto"",
                        ""Fornecedor"" = Origem.""Fornecedor"",
                        ""Peso"" = Origem.""Peso"",
                        ""Altura"" = Origem.""Altura"",
                        ""Largura"" = Origem.""Largura"",
                        ""Kit"" = Origem.""Kit""
                WHEN NOT MATCHED THEN
                    INSERT (
                        ""SKU"", ""Descricao_Produto"", ""Fornecedor"", 
                        ""Peso"", ""Altura"", ""Largura"", ""Kit""
                    )
                    VALUES (
                        Origem.""SKU"", Origem.""Descricao_Produto"", Origem.""Fornecedor"", 
                        Origem.""Peso"", Origem.""Altura"", Origem.""Largura"", Origem.""Kit""
                    );
                SELECT ""Id_Produto"" FROM public.""dbo.Tabela_Produto"" WHERE ""SKU"" = @SKU;
            ";
        }

        private string CadastrarCustoProdutoQuery()
        {
            return $@"
                MERGE INTO public.""dbo.Tabela_Custo_Produto"" AS Destino
                USING (
                    SELECT 
                        @IdProduto AS ""Id_Produto"", 
                        @DataCompra AS ""Data_Compra"", 
                        @TipoCompra AS ""Tipo_Compra"", 
                        @PrecoUnitario AS ""Preco_Unitario"", 
                        @CustosExtras AS ""Custos_Extras"",
                        @ICMS AS ""ICMS"",
                        @IPI AS ""IPI"",
                        @PisCofins AS ""PISCOFINS"",
                        @MvaAjustado AS ""MVAAjustado"",
                        @IcmsRetido AS ""ICMSRetido"",
                        @IcmsProprio AS ""ICMSProprio""
                ) AS Origem
                ON Destino.""Id_Produto"" = Origem.""Id_Produto""
                WHEN MATCHED THEN
                    UPDATE SET
                        ""Data_Compra"" = Origem.""Data_Compra"",
                        ""Tipo_Compra"" = Origem.""Tipo_Compra"",
                        ""Preco_Unitario"" = Origem.""Preco_Unitario"",
                        ""Custos_Extras"" = Origem.""Custos_Extras"",
                        ""ICMS"" = Origem.""ICMS"",
                        ""IPI"" = Origem.""IPI"",
                        ""PISCOFINS"" = Origem.""PISCOFINS"",
                        ""MVAAjustado"" = Origem.""MVAAjustado"",
                        ""ICMSRetido"" = Origem.""ICMSRetido"",
                        ""ICMSProprio"" = Origem.""ICMSProprio""
                WHEN NOT MATCHED THEN
                    INSERT (
                        ""Id_Produto"", ""Data_Compra"", ""Tipo_Compra"", 
                        ""Preco_Unitario"", ""Custos_Extras"", 
                        ""ICMS"", ""IPI"", ""PISCOFINS"", 
                        ""MVAAjustado"", ""ICMSRetido"", 
                        ""ICMSProprio""
                    )
                    VALUES (
                        Origem.""Id_Produto"", Origem.""Data_Compra"", Origem.""Tipo_Compra"", 
                        Origem.""Preco_Unitario"", Origem.""Custos_Extras"", 
                        Origem.""ICMS"", Origem.""IPI"", Origem.""PISCOFINS"", 
                        Origem.""MVAAjustado"", Origem.""ICMSRetido"", 
                        Origem.""ICMSProprio""
                    );
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
