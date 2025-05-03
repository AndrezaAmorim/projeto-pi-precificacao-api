using ProjetoPiPrecificacao.Models;
using ProjetoPiPrecificacao.Repository.Interface;

namespace ProjetoPiPrecificacao.Repository
{
    public class PrecificacaoRepository : IPrecificacaoRepository
    {
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
