using System.Data;

namespace ProjetoPiPrecificacao.Repository.Interface
{
    public interface IDbRunnerRepository
    {
        IDbConnection RecuperarConexao();
    }
}
