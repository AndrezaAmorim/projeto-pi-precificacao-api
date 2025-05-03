using ProjetoPiPrecificacao.Infra.Interface;
using ProjetoPiPrecificacao.Models;
using ProjetoPiPrecificacao.Repository.Interface;
using ProjetoPiPrecificacao.Repository.Queries;

namespace ProjetoPiPrecificacao.Repository
{
    public class PrecificacaoRepository : IPrecificacaoRepository
    {
        private readonly PrecificacaoRepositoryQueries queries;

        public PrecificacaoRepository(IDbConnectionFactory dbConnectionFactory)
        {
            queries = new PrecificacaoRepositoryQueries();
        }

        public bool Salvar(PrecificacaoModel model)
        {
            // Implementar a lógica de salvar a precificação aqui
            return true; // Retornar true se o salvamento for bem-sucedido, caso contrário, false
        }

        public PrecificacaoModel CalcularPreco(PrecificacaoModel model)
        {
            // Implementar a lógica de calcular o preço aqui
            return new PrecificacaoModel(); // Retornar o modelo de precificação calculado
        }
    }
}
