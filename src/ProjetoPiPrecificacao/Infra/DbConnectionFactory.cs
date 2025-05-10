using Microsoft.Data.SqlClient;
using Npgsql;
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
            return new NpgsqlConnection(StringConexao); 
        }

        public IDbConnection ObterConexaoExecute() 
        {
            return new NpgsqlConnection(StringConexao);
        }
    }
}