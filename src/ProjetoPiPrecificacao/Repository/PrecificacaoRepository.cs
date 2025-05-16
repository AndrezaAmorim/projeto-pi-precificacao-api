using Dapper;
using ProjetoPiPrecificacao.Helpers;
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
            filtros.Add("@IdProduto", model.IdProduto);
            filtros.Add("@PrecoSugeridoSTSP", model.PrecoSugeridoSTSP);
            filtros.Add("@PrecoVenda", model.PrecoVenda);
            filtros.Add("@Desconto", model.Desconto);
            filtros.Add("@PrecoDesconto", model.PrecoDesconto);
            filtros.Add("@MargemLiquida", model.MargemLiquida);
            filtros.Add("@MargemBruta", model.MargemBruta);
            filtros.Add("@Lucro", model.Lucro);
            filtros.Add("@DataAlteracaoPreco", TratamentoHelper.GetHoraBrasil());

            Run((IDbConnection connection, IDbTransaction transaction) =>
            {
                retorno = connection.Execute(sql: queries.Salvar, param: filtros, transaction: transaction) > 0;
            });

            return retorno;
        }
    }
}
