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
            filtros.Add("@Peso", model.Peso);
            filtros.Add("@Altura", model.Altura);
            filtros.Add("@Largura", model.Largura);
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
            filtros.Add("@PrecoUnitario", decimal.Parse(model.PrecoUnitario, CultureInfo.InvariantCulture));
            filtros.Add("@CustosExtras", decimal.Parse(model.CustosExtras, CultureInfo.InvariantCulture));
            filtros.Add("@ICMS", decimal.Parse(model.ICMS, CultureInfo.InvariantCulture));
            filtros.Add("@IPI", decimal.Parse(model.IPI, CultureInfo.InvariantCulture));
            filtros.Add("@PisCofins", decimal.Parse(model.PisCofins, CultureInfo.InvariantCulture));
            filtros.Add("@MvaAjustado", decimal.Parse(model.MvaAjustado, CultureInfo.InvariantCulture));
            filtros.Add("@IcmsRetido", decimal.Parse(model.IcmsRetido, CultureInfo.InvariantCulture));
            filtros.Add("@IcmsProprio", decimal.Parse(model.IcmsProprio, CultureInfo.InvariantCulture));

            Run((IDbConnection connection, IDbTransaction transaction) =>
            {
                retorno =  connection.Execute(sql: queries.CadastrarCustoProduto, param: filtros, transaction: transaction) > 0;
            });

            return retorno;
        }

        public ProdutoModel BuscarProdutoPorSku(string SKU)
        {
            var filtros = new Dictionary<string, object>();
            filtros.Add("@SKU", SKU);

            using (IDbConnection conexao = DbConnectionFactory.ObterConexao())
            {
                return conexao.Query<ProdutoModel>(sql: queries.BuscarProdutoPorSku, param: filtros).FirstOrDefault();
            }
        }
    }
}
