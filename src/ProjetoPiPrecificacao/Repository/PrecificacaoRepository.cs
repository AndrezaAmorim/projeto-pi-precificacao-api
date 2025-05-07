using Dapper;
using ProjetoPiPrecificacao.Infra.Interface;
using ProjetoPiPrecificacao.Models;
using ProjetoPiPrecificacao.Repository.Interface;
using ProjetoPiPrecificacao.Repository.Queries;
using System.Data;

namespace ProjetoPiPrecificacao.Repository
{
    public class PrecificacaoRepository : DbRunnerRepository, IPrecificacaoRepository
    {
        private readonly PrecificacaoRepositoryQueries queries;

        public PrecificacaoRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
            queries = new PrecificacaoRepositoryQueries();
        }

        public bool Salvar(PrecificacaoModel model)
        {
            bool retorno = false;
            var filtros = new Dictionary<string, object>();

            Run((IDbConnection connection, IDbTransaction transaction) =>
            {
                retorno = connection.Execute(sql: queries.Salvar, param: filtros, transaction: transaction) > 0;
            });

            return retorno;
        }

        public PrecificacaoModel CalcularPreco(PrecificacaoModel model)
        {
            var filtros = new Dictionary<string, object>();

            using (IDbConnection conexao = DbConnectionFactory.ObterConexao())
            {
                return conexao.Query<PrecificacaoModel>(sql: queries.CalcularPreco, param: filtros).FirstOrDefault();
            }
        }
    }
}
