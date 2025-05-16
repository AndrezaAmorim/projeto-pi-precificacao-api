using Dapper;
using ProjetoPiPrecificacao.Infra.Interface;
using ProjetoPiPrecificacao.Models;
using ProjetoPiPrecificacao.Repository.Interface;
using ProjetoPiPrecificacao.Repository.Queries;
using System.Data;
using System.Globalization;

namespace ProjetoPiPrecificacao.Repository
{
    public class ProdutoRepository : DbRunnerRepository, IProdutoRepository
    {
        private readonly ProdutoRepositoryQueries queries;

        public ProdutoRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
            queries = new ProdutoRepositoryQueries();
        }

        public int CadastrarProduto(ProdutoModel model)
        {
            int retorno = 0;
            var filtros = new Dictionary<string, object>();
            filtros.Add("@SKU", model.SKU);
            filtros.Add("@NomeProduto", model.NomeProduto);
            filtros.Add("@Fornecedor", model.Fornecedor);
            filtros.Add("@Peso", model.Peso == null ? 0 : model.Peso);
            filtros.Add("@Altura", model.Altura == null ? 0 : model.Altura);
            filtros.Add("@Largura", model.Largura == null ? 0 : model.Largura);
            filtros.Add("@Kit", model.Kit);

            Run((IDbConnection connection, IDbTransaction transaction) =>
            {
                retorno = connection.QueryFirst<int>(sql: queries.CadastrarProduto, param: filtros, transaction: transaction);
            });

            return retorno;
        }

        public bool CadastrarCustoProduto(ProdutoModel model)
        {
            bool retorno = false;
            var filtros = new Dictionary<string, object>();
            filtros.Add("@IdProduto", model.IdProduto);
            filtros.Add("@DataCompra", model.DataCompra);
            filtros.Add("@TipoCompra", model.TipoCompra);
            filtros.Add("@PrecoUnitario", model.PrecoUnitario);
            filtros.Add("@CustosExtras",  model.CustosExtras);
            filtros.Add("@ICMS", model.ICMS);
            filtros.Add("@IPI", model.IPI);
            filtros.Add("@PisCofins", model.PisCofins);
            filtros.Add("@MvaAjustado", model.MvaAjustado);
            filtros.Add("@IcmsRetido", model.IcmsRetido);
            filtros.Add("@IcmsProprio", model.IcmsProprio);

            Run((IDbConnection connection, IDbTransaction transaction) =>
            {
                retorno =  connection.Execute(sql: queries.CadastrarCustoProduto, param: filtros, transaction: transaction) > 0;
            });

            return retorno;
        }

        public PrecificacaoModel? BuscarProdutoPorSku(string SKU)
        {
            var filtros = new Dictionary<string, object>();
            filtros.Add("@SKU", SKU);

            using (IDbConnection conexao = DbConnectionFactory.ObterConexao())
            {
                return conexao.Query<PrecificacaoModel>(sql: queries.BuscarProdutoPorSku, param: filtros).FirstOrDefault();
            }
        }
    }
}
