using ProjetoPiPrecificacao.Models;

namespace ProjetoPiPrecificacao.Repository.Interface
{
    public interface IPrecificacaoRepository
    {
        PrecificacaoModel CalcularPreco(PrecificacaoModel model);
        bool Salvar(PrecificacaoModel model);
    }
}
