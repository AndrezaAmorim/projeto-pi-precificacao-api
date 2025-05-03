using Microsoft.Data.SqlClient;
using ProjetoPiPrecificacao.Infra.Interface;
using System.Data;

namespace ProjetoPiPrecificacao.Infra
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private string StringConexao { get; set; }

        public DbConnectionFactory(string stringConexao)
        {
            StringConexao = stringConexao;
        }

        public IDbConnection ObterConexao() 
        {
            return new SqlConnection(StringConexao); 
        }

        public IDbConnection ObterConexaoExecute() 
        {
            return new SqlConnection(StringConexao);
        }
    }
}