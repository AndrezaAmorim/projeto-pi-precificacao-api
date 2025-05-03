using ProjetoPiPrecificacao.Business;
using ProjetoPiPrecificacao.Business.Interface;

namespace ProjetoPiPrecificacao
{
    public class InjecaoDependencia
    {
        public static void Configurar(IConfiguration configuration, IServiceCollection services)
        {
            services.AddScoped<IPrecificacaoBusiness, PrecificacaoBusiness>();
            services.AddScoped<IProdutoBusiness, ProdutoBusiness>();
        }
    }
}
