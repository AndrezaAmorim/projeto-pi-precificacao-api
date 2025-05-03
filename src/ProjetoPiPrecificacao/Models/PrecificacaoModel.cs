namespace ProjetoPiPrecificacao.Models
{
    public class PrecificacaoModel : ProdutoModel
    {
        public int IdPrecoProduto { get; set; }
        public float PrecoSugeridoSTSP { get; set; }
        public float PrecoVenda { get; set; }
        public float Desconto { get; set; }
        public float PrecoDesconto { get; set; }
        public float MargemLiquida { get; set; }
        public float MargemBruta { get; set; }
        public float Lucro { get; set; }
        public DateTime DataAlteracaoPreco { get; set; }
    }
}