using Npgsql;
using ProjetoPiPrecificacao.Business;
using ProjetoPiPrecificacao.Business.Interface;
using ProjetoPiPrecificacao.Infra;
using ProjetoPiPrecificacao.Infra.Interface;
using ProjetoPiPrecificacao.Repository;
using ProjetoPiPrecificacao.Repository.Interface;
using System.Data;

namespace ProjetoPiPrecificacao
{
    public class InjecaoDependencia
    {
        public static void Configurar(IConfiguration configuration, IServiceCollection services)
        {
            #region Business
            services.AddScoped<IPrecificacaoBusiness, PrecificacaoBusiness>();
            services.AddScoped<IProdutoBusiness, ProdutoBusiness>();
            #endregion

            #region Repository
            services.AddScoped<IPrecificacaoRepository, PrecificacaoRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IDbRunnerRepository, DbRunnerRepository>();
            #endregion

            #region Infra
            services.AddTransient<IDbConnectionFactory>((connection) =>
            {
                string connectionString = configuration.GetConnectionString("MinhaConexao");
                return new DbConnectionFactory(connectionString);
            });
            #endregion
        }
    }
}
