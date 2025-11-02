using OfficeOpenXml;
using ProjetoPiPrecificacao.Business.Interface;
using ProjetoPiPrecificacao.Models;
using ProjetoPiPrecificacao.Repository.Interface;
using System.Globalization;

namespace ProjetoPiPrecificacao.Business
{
    public class ProdutoBusiness : IProdutoBusiness
    {
        private readonly IProdutoRepository _produtoRepository;
        public ProdutoBusiness(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public bool Cadastrar(ProdutoModel model)
        {
            try 
            {
                int idProduto = _produtoRepository.CadastrarProduto(model);

                if (idProduto == 0)
                    throw new Exception("Erro ao cadastrar produto");

                model.IdProduto = idProduto;
                bool retorno = _produtoRepository.CadastrarCustoProduto(model);

                if (!retorno)
                    throw new Exception("Erro ao cadastrar custo do produto");

                return retorno;

            } catch (Exception ex) 
            {
                throw new Exception($"Erro ao cadastrar produto. {ex.Message}");
            }
        }

        public ProdutoModel? BuscarProdutoPorSku(string SKU)
        {
            return _produtoRepository.BuscarProdutoPorSku(SKU);
        }

        public async Task CadastrarProdutoExcel(IFormFile arquivo)
        {
            ExcelPackage.License.SetNonCommercialPersonal("ProjetoPi");

            var produtos = new List<ProdutoModel>();
            var erros = new List<string>();

            using (var stream = new MemoryStream())
            {
                await arquivo.CopyToAsync(stream);
                stream.Position = 0;

                using (var pacote = new ExcelPackage(stream))
                {
                    var worksheet = pacote.Workbook.Worksheets[0];

                    var dimension = worksheet.Dimension;
                    var totalRows = dimension?.Rows ?? 0;
                    var totalCols = dimension?.Columns ?? 0;

                    if (totalRows > 1)
                    {
                        for (int row = 2; row <= totalRows; row++) 
                        {
                            try
                            {
                                bool linhaVazia = true;

                                for (int col = 1; col <= totalCols; col++)
                                {
                                    var cellValue = worksheet.Cells[row, col].Text.Trim();
                                    if (!string.IsNullOrWhiteSpace(cellValue))
                                    {
                                        linhaVazia = false;
                                        break; 
                                    }
                                }

                                if (!linhaVazia)
                                {
                                    var produto = new ProdutoModel
                                    {
                                        SKU = ValidarCampoTexto(worksheet.Cells[row, 1].Text, row, "SKU"),
                                        NomeProduto = ValidarCampoTexto(worksheet.Cells[row, 2].Text, row, "Descrição do Produto"),
                                        Fornecedor = worksheet.Cells[row, 3].Text.Trim(),
                                        Peso = ParseFloatComUnidade(worksheet.Cells[row, 4].Text, "KG", row, "Peso"),
                                        Altura = ParseFloat(worksheet.Cells[row, 5].Text, row, "Altura"),
                                        Largura = ParseFloat(worksheet.Cells[row, 6].Text, row, "Largura"),
                                        Kit = ValidarCampoKit(worksheet.Cells[row, 7].Text, row),
                                        DataCompra = ValidarCampoData(worksheet.Cells[row, 8].Text, row),
                                        TipoCompra = worksheet.Cells[row, 9].Text.Trim(),
                                        PrecoUnitario = ParseFloatComUnidade(worksheet.Cells[row, 10].Text, "R$", row, "Preço Unitário"),
                                        CustosExtras = ParseFloatComUnidade(worksheet.Cells[row, 11].Text, "R$", row, "Custos Extras"),
                                        ICMS = ParseFloatComUnidade(worksheet.Cells[row, 12].Text, "%", row, "ICMS"),
                                        IPI = ParseFloatComUnidade(worksheet.Cells[row, 13].Text, "%", row, "IPI"),
                                        PisCofins = ParseFloatComUnidade(worksheet.Cells[row, 14].Text, "%", row, "Pis Cofins"),
                                        MvaAjustado = ParseFloatComUnidade(worksheet.Cells[row, 15].Text, "%", row, "MVA Ajustado"),
                                        IcmsRetido = ParseFloatComUnidade(worksheet.Cells[row, 16].Text, "%", row, "ICMS Retido"),
                                        IcmsProprio = ParseFloatComUnidade(worksheet.Cells[row, 17].Text, "%", row, "ICMS Próprio")
                                    };

                                    produtos.Add(produto);
                                }
                            }
                            catch (Exception ex)
                            {
                                erros.Add($"Erro na linha {row}: {ex.Message}");
                            }
                        }
                    }

                    foreach (var produto in produtos)
                    {
                        var result = Cadastrar(produto);
                        if (!result)
                            throw new Exception("Erro ao cadastrar produtos");
                    }
                }
            }
        }

        private string ValidarCampoTexto(string valor, int linha, string nomeCampo)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                throw new Exception($"Linha {linha}: O campo '{nomeCampo}' é obrigatório.");
            }
            return valor.Trim();
        }

        private bool ValidarCampoKit(string valor, int linha)
        {
            if (string.IsNullOrWhiteSpace(valor) || (valor.ToLower() != "sim"))
            {
                return false;
            }
            return true;
        }

        private DateTime ValidarCampoData(string valor, int linha)
        {
            if (string.IsNullOrWhiteSpace(valor) || !DateTime.TryParseExact(valor, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dataCompra))
            {
                throw new Exception($"Linha {linha}: O campo 'Data da Compra' é obrigatório e precisa estar no formato dd/MM/yyyy.");
            }
            return dataCompra;
        }

        private float ParseFloatComUnidade(string valor, string unidade, int linha, string nomeCampo)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                if (nomeCampo != "Peso")
                    throw new Exception($"Linha {linha}: O campo '{nomeCampo}' é obrigatório.");

                return 0;
            }

            valor = valor.ToLower().Replace(unidade.ToLower(), "").Trim();
            valor = valor.Replace(",", ".").Trim();
            if (float.TryParse(valor, out float resultado))
            {
                if (nomeCampo == "Peso" || nomeCampo == "Preço Unitário" || nomeCampo == "Custos Extras")
                {
                    return resultado / 100;
                }

                return resultado;
            }
            throw new Exception($"Linha {linha}: O campo '{nomeCampo}' deve ser um número válido.");
        }

        private float? ParseFloat(string valor, int linha, string nomeCampo)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                return null;
            }

            valor = valor.Replace(",", ".").Trim(); 
            if (float.TryParse(valor, out float resultado))
            {
                return resultado;
            }

            throw new Exception($"Linha {linha}: O campo '{nomeCampo}' deve ser um número válido.");
        }
    }
}
