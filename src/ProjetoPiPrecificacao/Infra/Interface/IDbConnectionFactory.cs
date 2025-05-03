using System.Data;

namespace ProjetoPiPrecificacao.Infra.Interface
{
    public interface IDbConnectionFactory
    {
        IDbConnection ObterConexao();
        IDbConnection ObterConexaoExecute();
    }
}
