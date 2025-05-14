using ProjetoPiPrecificacao.Models;

namespace ProjetoPiPrecificacao.Repository.Interface
{
    public interface IPrecificacaoRepository
    {
        bool Salvar(PrecificacaoModel model);
    }
}
