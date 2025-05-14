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
            PrecificacaoModel? produtoModel = _produtoRepository.BuscarProdutoPorSku(model.SKU);
            if (produtoModel == null)
                throw new Exception("Produto não encontrado.");

            float margemLucroDesejada = 40;
            float? custos = CalcularPorcentagem(produtoModel.ICMS, produtoModel.PrecoUnitario) +
                CalcularPorcentagem(produtoModel.IPI, produtoModel.PrecoUnitario) +
                CalcularPorcentagem(produtoModel.PisCofins, produtoModel.PrecoUnitario) +
                CalcularPorcentagem(produtoModel.MvaAjustado, produtoModel.PrecoUnitario) +
                CalcularPorcentagem(produtoModel.IcmsRetido, produtoModel.PrecoUnitario) +
                CalcularPorcentagem(produtoModel.IcmsProprio, produtoModel.PrecoUnitario) +
                produtoModel.CustosExtras + produtoModel.PrecoUnitario;

            produtoModel.PrecoSugeridoSTSP = custos / (1 - (margemLucroDesejada/100));
            
            if (model.PrecoVenda > 0)
            {
                if (model.Desconto > 0)
                {
                    float? valor = CalcularPorcentagem(model.Desconto, model.PrecoVenda);
                    produtoModel.PrecoDesconto = (model.PrecoVenda - valor) <= 0 ? 0 : (model.PrecoVenda - valor);
                    produtoModel.Desconto = model.Desconto;
                }

                produtoModel.MargemLiquida = CalcularMargemLiquida(model.PrecoVenda, custos);
                produtoModel.MargemBruta = CalcularMargemBruta(model.PrecoVenda, custos);
                produtoModel.Lucro = CalcularLucro(model.PrecoVenda, custos);

            } else
            {
                if (model.Desconto > 0)
                {
                    float? valor = CalcularPorcentagem(model.Desconto, produtoModel.PrecoSugeridoSTSP);
                    produtoModel.PrecoDesconto = (produtoModel.PrecoSugeridoSTSP - valor) <= 0 ? 0 : (produtoModel.PrecoSugeridoSTSP - valor);
                    produtoModel.Desconto = model.Desconto;
                }

                produtoModel.MargemLiquida = CalcularMargemLiquida(produtoModel.PrecoSugeridoSTSP, custos);
                produtoModel.MargemBruta = CalcularMargemBruta(produtoModel.PrecoSugeridoSTSP, custos);
                produtoModel.Lucro = CalcularLucro(produtoModel.PrecoSugeridoSTSP, custos);
            }

            if (produtoModel.DataAlteracaoPreco == null)
                produtoModel.DataAlteracaoPreco = DateTime.Now;

            return produtoModel;
        }

        private float? CalcularPorcentagem(float? valor, float? precoUnitario)
        {
            float? retorno = precoUnitario* valor / 100;
            if (retorno < 0)
                retorno = 0;

            return retorno;
        }

        private float? CalcularMargemLiquida(float? precoVenda, float? custoTotal)
        {
            float? lucroLiquido = precoVenda - custoTotal;
            float? retorno = (lucroLiquido / precoVenda) * 100;
            if (retorno < 0)
                retorno = 0;

            return retorno;
        }

        private float? CalcularMargemBruta(float? precoVenda, float? custos)
        {
            float? receitaLiquida = precoVenda - custos;
            float? lucroBruto = receitaLiquida - custos;
            float? retorno = (lucroBruto / receitaLiquida) * 100;
            if (retorno < 0)
                retorno = 0;

            return retorno;
        }

        private float? CalcularLucro(float? precoVenda, float? custoTotal)
        {
            float? retorno = precoVenda - custoTotal;
            if (retorno < 0)
                retorno = 0;

            return retorno;
        }
    }
}
