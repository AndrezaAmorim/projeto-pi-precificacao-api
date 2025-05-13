using ProjetoPiPrecificacao.Business.Interface;
using ProjetoPiPrecificacao.Models;
using ProjetoPiPrecificacao.Repository.Interface;

namespace ProjetoPiPrecificacao.Business
{
    public class PrecificacaoBusiness : IPrecificacaoBusiness
    {
        private readonly IPrecificacaoRepository _precificacaoRepository;
        private readonly IProdutoRepository _produtoRepository;

        public PrecificacaoBusiness(IPrecificacaoRepository precificacaoRepository, IProdutoRepository produtoRepository)
        {
            _precificacaoRepository = precificacaoRepository;
            _produtoRepository = produtoRepository;
        }

        public bool Salvar(PrecificacaoModel model)
        {
            return _precificacaoRepository.Salvar(model);
        }

        public PrecificacaoModel CalcularPreco(PrecificacaoModel model)
        {
            ProdutoModel? produtoModel = _produtoRepository.BuscarProdutoPorSku(model.SKU);
            if (produtoModel == null)
                throw new Exception("Produto não encontrado.");

            float margemLucroDesejada = 40;
            float custos = CalcularPorcentagem(model.ICMS, produtoModel.PrecoUnitario) +
                CalcularPorcentagem(model.IPI, produtoModel.PrecoUnitario) +
                CalcularPorcentagem(model.PisCofins, produtoModel.PrecoUnitario) +
                CalcularPorcentagem(model.MvaAjustado, produtoModel.PrecoUnitario) +
                CalcularPorcentagem(model.IcmsRetido, produtoModel.PrecoUnitario) +
                CalcularPorcentagem(model.IcmsProprio, produtoModel.PrecoUnitario) +
                model.CustosExtras;

            model.PrecoSugeridoSTSP = custos / (1 - margemLucroDesejada);
            
            if (model.PrecoVenda > 0)
            {
                if (model.Desconto > 0)
                {
                    float valor = CalcularPorcentagem(model.Desconto, model.PrecoVenda);
                    model.PrecoDesconto = (model.PrecoVenda - valor) <= 0 ? 0 : (model.PrecoVenda - valor);
                }

                model.MargemLiquida = CalcularMargemLiquida(model.PrecoVenda, custos);
                model.MargemBruta = CalcularMargemBruta(model.PrecoVenda, custos);
                model.Lucro = CalcularLucro(model.PrecoVenda, custos);

            } else
            {
                if (model.Desconto > 0)
                {
                    float valor = CalcularPorcentagem(model.Desconto, model.PrecoSugeridoSTSP);
                    model.PrecoDesconto = (model.PrecoSugeridoSTSP - valor) <= 0 ? 0 : (model.PrecoSugeridoSTSP - valor);
                }

                model.MargemLiquida = CalcularMargemLiquida(model.PrecoSugeridoSTSP, custos);
                model.MargemBruta = CalcularMargemBruta(model.PrecoSugeridoSTSP, custos);
                model.Lucro = CalcularLucro(model.PrecoSugeridoSTSP, custos);
            }

            return _precificacaoRepository.CalcularPreco(model);
        }

        private float CalcularPorcentagem(float valor, float precoUnitario)
        {
            float retorno = precoUnitario* valor / 100;
            if (retorno < 0)
                retorno = 0;

            return retorno;
        }

        private float CalcularMargemLiquida(float precoVenda, float custoTotal)
        {
            float lucroLiquido = precoVenda - custoTotal;
            float retorno = (lucroLiquido / precoVenda) * 100;
            if (retorno < 0)
                retorno = 0;

            return retorno;
        }

        private float CalcularMargemBruta(float precoVenda, float custos)
        {
            float receitaLiquida = precoVenda - custos;
            float lucroBruto = receitaLiquida - custos;
            float retorno = (lucroBruto / receitaLiquida) * 100;
            if (retorno < 0)
                retorno = 0;

            return retorno;
        }

        private float CalcularLucro(float precoVenda, float custoTotal)
        {
            float retorno = precoVenda - custoTotal;
            if (retorno < 0)
                retorno = 0;

            return retorno;
        }
    }
}
