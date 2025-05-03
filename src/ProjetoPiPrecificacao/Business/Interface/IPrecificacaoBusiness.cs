using ProjetoPiPrecificacao.Models;

namespace ProjetoPiPrecificacao.Business.Interface
{
    public interface IPrecificacaoBusiness
    {
        bool Salvar(PrecificacaoModel model);
        PrecificacaoModel CalcularPreco(PrecificacaoModel model);
    }
}
