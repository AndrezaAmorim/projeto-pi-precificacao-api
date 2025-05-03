using ProjetoPiPrecificacao.Business.Interface;
using ProjetoPiPrecificacao.Models;
using ProjetoPiPrecificacao.Repository.Interface;

namespace ProjetoPiPrecificacao.Business
{
    public class PrecificacaoBusiness : IPrecificacaoBusiness
    {
        private readonly IPrecificacaoRepository _precificacaoRepository;

        public PrecificacaoBusiness(IPrecificacaoRepository precificacaoRepository)
        {
            _precificacaoRepository = precificacaoRepository;
        }

        public bool Salvar(PrecificacaoModel model)
        {
            return _precificacaoRepository.Salvar(model);
        }

        public PrecificacaoModel CalcularPreco(PrecificacaoModel model)
        {
            return _precificacaoRepository.CalcularPreco(model);
        }
    }
}
