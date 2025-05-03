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
        public float PrecoUnitario { get; set; }
        public float CustosExtras { get; set; }
        public float ICMS { get; set; }
        public float IPI { get; set; }
        public float PisCofins { get; set; }
        public float MvaAjustado { get; set; }
        public float IcmsRetido { get; set; }
        public float IcmsProprio { get; set; }
    }
}