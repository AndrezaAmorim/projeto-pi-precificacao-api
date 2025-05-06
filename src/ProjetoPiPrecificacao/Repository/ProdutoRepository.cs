using Dapper;
using ProjetoPiPrecificacao.Infra.Interface;
using ProjetoPiPrecificacao.Models;
using ProjetoPiPrecificacao.Repository.Interface;
using ProjetoPiPrecificacao.Repository.Queries;
using System.Data;

namespace ProjetoPiPrecificacao.Repository
{
    public class ProdutoRepository : DbRunnerRepository, IProdutoRepository
    {
        private readonly ProdutoRepositoryQueries queries;

        public ProdutoRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
            queries = new ProdutoRepositoryQueries();
        }

        public bool Cadastrar(ProdutoModel model)
        {
            bool retorno = false;
            var filtros = new Dictionary<string, object>();

            Run((IDbConnection connection, IDbTransaction transaction) =>
            {
                retorno =  connection.Execute(queries.Cadastrar, filtros, transaction) > 0;
            });

            return retorno;
        }

        public ProdutoModel BuscarProdutoPorSku(string SKU)
        {
            var filtros = new Dictionary<string, object>();
            filtros.Add("@SKU", SKU);

            using (IDbConnection conexao = DbConnectionFactory.ObterConexao())
            {
                return conexao.Query<ProdutoModel>(queries.BuscarProdutoPorSku, filtros).FirstOrDefault();
            }
        }
    }
}
