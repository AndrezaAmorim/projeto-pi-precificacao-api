using ProjetoPiPrecificacao.Infra.Interface;
using ProjetoPiPrecificacao.Repository.Interface;
using System.Data;

namespace ProjetoPiPrecificacao.Repository
{
    public class DbRunnerRepository : IDbRunnerRepository
    {
        protected IDbConnectionFactory DbConnectionFactory { get; private set; }

        public DbRunnerRepository(IDbConnectionFactory dbConnectionFactory)
        {
            DbConnectionFactory = dbConnectionFactory;
        }

        public IDbConnection RecuperarConexao()
        {
            return DbConnectionFactory.ObterConexaoExecute();
        }

        protected bool Run(Action<IDbConnection, IDbTransaction> action)
        {
            using (IDbConnection connection = RecuperarConexao())
            {
                connection.Open();
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        action?.Invoke(connection, transaction);
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Erro ao executar a ação: {ex.Message}", ex);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
    }
}
