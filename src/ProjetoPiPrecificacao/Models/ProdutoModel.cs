namespace ProjetoPiPrecificacao.Models
{
    public class ProdutoModel
    {
        public int IdProduto { get; set; }
        public int IdCustoProduto { get; set; }
        public string SKU { get; set; }
        public string NomeProduto { get; set; }
        public string Fornecedor { get; set; }
        public float Peso { get; set; }
        public float Altura { get; set; }
        public float Largura { get; set; }
        public bool Kit { get; set; }
        public DateTime DataCompra { get; set; }
        public string TipoCompra { get; set; }
        public string PrecoUnitario { get; set; }
        public string CustosExtras { get; set; }
        public string ICMS { get; set; }
        public string IPI { get; set; }
        public string PisCofins { get; set; }
        public string MvaAjustado { get; set; }
        public string IcmsRetido { get; set; }
        public string IcmsProprio { get; set; }
    }
}