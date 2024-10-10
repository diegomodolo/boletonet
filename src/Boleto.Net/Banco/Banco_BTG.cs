using System;
using System.Linq;
using System.Text;
using BoletoNet.Util;

namespace BoletoNet
{
    internal class Banco_BTG : AbstractBanco, IBanco
    {
        internal Banco_BTG()
        {
            this.Codigo = 208;
            this.Digito = "0";
            this.Nome = "BANCO BTG PACTUAL S.A.";
        }

        public override string GerarHeaderRemessa(string numeroConvenio, Cedente cedente, TipoArquivo tipoArquivo,
            int numeroArquivoRemessa,
            Boleto boletos)
        {
            try
            {
                string _header = " ";

                base.GerarHeaderRemessa("0", cedente, tipoArquivo, numeroArquivoRemessa);

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        _header = GerarHeaderRemessaCNAB240(cedente, numeroArquivoRemessa);
                        break;
                    case TipoArquivo.CNAB400:
                        throw new Exception("Tipo de arquivo não implementado.");
                    case TipoArquivo.Outro:
                        throw new Exception("Tipo de arquivo inexistente.");
                }

                return _header;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a geração do HEADER do arquivo de REMESSA.", ex);
            }
        }

        public override string GerarHeaderLoteRemessa(string numeroConvenio, Cedente cedente, int numeroArquivoRemessa,
            TipoArquivo tipoArquivo)
        {
            try
            {
                string header = " ";

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        header = GerarHeaderLoteRemessaCNAB240(cedente, numeroArquivoRemessa);
                        break;
                    case TipoArquivo.CNAB400:
                        throw new Exception("Tipo de arquivo não implementado.");
                    case TipoArquivo.Outro:
                        throw new Exception("Tipo de arquivo inexistente.");
                }

                return header;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a geração do HEADER DO LOTE do arquivo de REMESSA.", ex);
            }
        }

        public override string GerarDetalheRemessa(Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
        {
            try
            {
                string _detalhe = " ";

                //base.GerarDetalheRemessa(boleto, numeroRegistro, tipoArquivo);

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        _detalhe = GerarDetalheRemessaCNAB240(boleto, numeroRegistro, tipoArquivo);
                        break;
                    case TipoArquivo.CNAB400:
                        throw new Exception("Tipo de arquivo não implementado.");
                    case TipoArquivo.Outro:
                        throw new Exception("Tipo de arquivo inexistente.");
                }

                return _detalhe;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a geração do DETALHE arquivo de REMESSA.", ex);
            }
        }

        public string GerarDetalheRemessaCNAB240(Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
        {
            try
            {
                var detalhe = new StringBuilder();
                detalhe.Append(Utils.FormatCode(Codigo.ToString(), "0", 3, true)); // Código do banco
                detalhe.Append("0001"); // Lote de serviço
                detalhe.Append("3"); // Tipo de registro
                detalhe.Append(Utils.FormatCode(numeroRegistro.ToString(), 5)); // Número do registro
                detalhe.Append("P"); // Segmento P
                detalhe.Append(" "); // Uso exclusivo
                detalhe.Append("01"); // Tipo de movimento
                detalhe.Append(Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia, "0", 5, true)); // Agência
                detalhe.Append(" "); // Dígito verificador da agência
                detalhe.Append(Utils.FormatCode(boleto.Cedente.ContaBancaria.Conta, "0", 12, true)); // Conta
                detalhe.Append(boleto.Cedente.ContaBancaria.DigitoConta); // Dígito verificador da conta
                detalhe.Append(" "); // Dígito verificador da agência/conta
                detalhe.Append(Utils.FormatCode(boleto.NossoNumero.Replace("/", "").Replace("-", ""),
                    20)); // Nosso número
                detalhe.Append("1"); // Código da carteira
                detalhe.Append(0); // Forma de cadastro do título no banco
                detalhe.Append("0"); // tipo de documento
                detalhe.Append("0"); // identificação da emissão do boleto de pagamento
                detalhe.Append("0"); // identificação da distribuição
                detalhe.Append(Utils.FormatCode(boleto.NumeroDocumento, 15)); // Número do documento de cobrança
                detalhe.Append(boleto.DataVencimento.ToString("ddMMyyyy")); // Data de vencimento
                detalhe.Append(Utils.FormatCode(boleto.ValorBoleto.ToString("f").Replace(",", "").Replace(".", ""),
                    15)); // Valor do título
                detalhe.Append("00000"); // Agência encarregada da cobrança
                detalhe.Append(" "); // Dígito verificador da agência
                detalhe.Append("01"); // Espécie de título
                detalhe.Append(boleto.Aceite.ToUpper() == "S" ? "A" : "N"); // Aceite
                detalhe.Append(boleto.DataDocumento.ToString("ddMMyyyy")); // Data de emissão
                detalhe.Append("1"); // valor por dia
                detalhe.Append(boleto.DataJurosMora.ToString("ddMMyyyy")); // Data dos juros de mora
                detalhe.Append(boleto.JurosMora.ToString("f").Replace(",", "")
                    .Replace(".", "")); // Juros de mora por dia
                detalhe.Append("0");
                detalhe.Append(boleto.DataDesconto.ToString("ddMMyyyy")); // Data do desconto
                detalhe.Append(boleto.ValorDesconto.ToString("f").Replace(",", "")
                    .Replace(".", "")); // Valor do desconto
                detalhe.Append(boleto.IOF.ToString("f").Replace(",", "").Replace(".", "")); // Valor do IOF
                detalhe.Append(boleto.Abatimento.ToString("f").Replace(",", "")
                    .Replace(".", "")); // Valor do abatimento
                detalhe.Append(Utils.FormatCode(boleto.NumeroDocumento, " ", 25, false));
                detalhe.Append(boleto.Instrucoes[0].Codigo); // Código da primeira instrução
                detalhe.Append(boleto.Instrucoes[0].QuantidadeDias); // Quantidade de dias da primeira instrução
                detalhe.Append(boleto.Instrucoes[1].Codigo); // Código da segunda instrução
                detalhe.Append(boleto.Instrucoes[1].QuantidadeDias); // Quantidade de dias da segunda instrução
                detalhe.Append("09"); // código da moeda
                detalhe.Append("0000000000"); // Número do contrato da operação de crédito
                detalhe.Append(" "); // Uso exclusivo do banco

                return Utils.SubstituiCaracteresEspeciais(detalhe.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao gerar DETALHE do arquivo CNAB240.", e);
            }
        }

        private string GerarHeaderRemessaCNAB240(Cedente cedente, int numeroArquivoRemessa)
        {
            try
            {
                var header = new StringBuilder();
                header.Append(this.Codigo); // Código do banco
                header.Append("0000"); // lote de serviço, sempre 0000
                header.Append("3"); // tipo de registro, sempre 3
                header.Append(Utils.FormatCode("", " ", 9)); // uso exclusivo, 9 espaços
                header.Append(cedente.CPFCNPJ.Length == 11 ? "1" : "2"); // tipo de inscrição
                header.Append(Utils.FormatCode(cedente.CPFCNPJ, "0", 14, true)); // inscrição
                header.Append(Utils.FormatCode(cedente.Convenio.ToString(), " ", 20)); // convênio
                header.Append(Utils.FormatCode(cedente.ContaBancaria.Agencia, "0", 5, true)); // agência
                header.Append(" "); // dígito verificador da agência
                header.Append(Utils.FormatCode(cedente.ContaBancaria.Conta, "0", 12, true)); // conta
                header.Append(cedente.ContaBancaria.DigitoConta); // dígito verificador da conta
                header.Append(" "); // dígito verificador da agência/conta
                header.Append(Utils.FormatCode(cedente.Nome, " ", 30)); // nome da empresa
                header.Append(Utils.FormatCode(this.Nome, " ", 30)); // nome do banco
                header.Append(Utils.FormatCode("", " ", 10)); // uso exclusivo, 10 espaços
                header.Append("1"); // código de remessa/retorno
                header.Append(DateTime.Now.ToString("ddMMyyyyHHmmss")); // data/hora de geração do arquivo
                header.Append(Utils.FitStringLength(numeroArquivoRemessa.ToString(), 6, 6, '0', 0, true, true, true));
                header.Append("083"); // versão do arquivo
                header.Append("00000"); // densidade
                header.Append(Utils.FormatCode("", " ", 69)); // uso reservado
                var headerFormatado = Utils.SubstituiCaracteresEspeciais(header.ToString());
                return headerFormatado;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB240.", ex);
            }
        }

        private string GerarHeaderLoteRemessaCNAB240(Cedente cedente, int numeroArquivoRemessa)
        {
            try
            {
                var header = new StringBuilder();

                header.Append(Utils.FormatCode(Codigo.ToString(), "0", 3, true)); // código do banco
                header.Append("0001"); // lote de serviço (sempre 0001)
                header.Append("1"); // tipo de registro (sempre 1)
                header.Append("R"); // tipo de operação (R - remessa)
                header.Append("01"); // tipo de serviço (01 - cobrança)
                header.Append("  "); // uso exclusivo
                header.Append("000"); // versão do layout do lote
                header.Append(" "); // uso exclusivo
                header.Append(cedente.CPFCNPJ.Length == 11 ? "1" : "2"); // tipo de inscrição
                header.Append(Utils.FormatCode(cedente.CPFCNPJ, "0", 15, true)); // inscrição
                header.Append(Utils.FormatCode(cedente.Convenio.ToString(), "0", 20, true)); // convênio
                header.Append(Utils.FormatCode(cedente.ContaBancaria.Agencia, "0", 5, true)); // agência
                header.Append(Utils.FormatCode(cedente.ContaBancaria.Conta, "0", 11, true)); // conta
                header.Append(cedente.ContaBancaria.DigitoConta); // dígito verificador da conta
                header.Append(" "); // uso exclusivo
                header.Append(Utils.FormatCode(cedente.Nome, " ", 30)); // nome da empresa
                header.Append(Utils.FormatCode("", " ", 40)); // mensagem 1
                header.Append(Utils.FormatCode("", " ", 40)); // mensagem 2
                header.Append(Utils.FormatCode(numeroArquivoRemessa.ToString(), "0", 8,
                    true)); // número remessa/retorno
                header.Append(Utils.FormatCode("", "0", 8)); // data da gravação do arquivo
                header.Append(Utils.FormatCode("", "0", 8)); // data do crédito
                header.Append(Utils.FormatCode("", " ", 33)); // uso exclusivo

                return Utils.SubstituiCaracteresEspeciais(header.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao gerar HEADER DO LOTE do arquivo de remessa.", e);
            }
        }

        public override string GerarDetalheSegmentoPRemessa(Boleto boleto, int numeroRegistro, string numeroConvenio)
        {
            try
            {
                string _segmentoP;

                //Código do Banco na compensação ==> 001-003
                _segmentoP = Utils.FormatCode(Codigo.ToString(), "0", 3, true);

                //Numero do lote remessa ==> 004 - 007
                _segmentoP += Utils.FitStringLength("1", 4, 4, '0', 0, true, true, true);

                //Tipo de registro => 008 - 008
                _segmentoP += "3";

                //Nº seqüencial do registro de lote ==> 009 - 013
                _segmentoP += Utils.FitStringLength(numeroRegistro.ToString(), 5, 5, '0', 0, true, true, true);

                //Cód. Segmento do registro detalhe ==> 014 - 014
                _segmentoP += "P";

                //Reservado (uso Banco) ==> 015 - 015
                _segmentoP += " ";

                //Código de movimento remessa ==> 016 - 017
                _segmentoP += ObterCodigoDaOcorrencia(boleto);

                //Agência do Cedente ==> 018 – 022
                _segmentoP +=
                    Utils.FitStringLength(boleto.Cedente.ContaBancaria.Agencia, 5, 5, '0', 0, true, true, true);

                //Dígito da Agência do Cedente ==> 023 – 023
                _segmentoP += " ";

                //Número da conta corrente ==> 024 - 035
                _segmentoP +=
                    Utils.FitStringLength(boleto.Cedente.ContaBancaria.Conta, 12, 12, '0', 0, true, true, true);

                //Dígito verificador da conta ==> 036 – 036
                _segmentoP += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoConta, 1, 1, '0', 0, true, true,
                    true);

                //Dígito verificador da coop/ag/conta ==> 037 - 037
                _segmentoP += " ";

                boleto.Valida();

                //Nosso número ==> 038 - 057
                ////_segmentoP += Utils.FitStringLength(boleto.NossoNumero.Replace("-", "").Replace("/", ""), 20, 20, ' ', 0, true, true, true);
                _segmentoP += boleto.NossoNumero.Replace("-", "").Replace("/", "").PadRight(20, ' ');

                //Carteira ==> 058 - 058
                _segmentoP += "1";

                //Forma de cad. do título no banco ==> 059 - 059
                _segmentoP += "1";

                //Tipo de documento ==> 060 - 060
                _segmentoP += "1";

                //Ident. emissão do boleto ==> 061 - 061
                _segmentoP += "2";

                //Identificação da distribuição ==> 062 - 062
                _segmentoP += "2";

                //Nº do documento ==> 063 - 077
                _segmentoP += Utils.FitStringLength(boleto.NumeroDocumento, 15, 15, ' ', 0, true, true, false);

                //Data de vencimento do título ==> 078 - 085
                _segmentoP += boleto.DataVencimento.ToString("ddMMyyyy");

                //Valor nominal do título ==> 086 - 100
                _segmentoP +=
                    Utils.FitStringLength(boleto.ValorBoleto.ApenasNumeros(), 15, 15, '0', 0, true, true, true);

                //Agência encarregada da cobrança ==> 101 - 104
                _segmentoP += "0000";

                //Dígito da Agência do Cedente ==> 105 – 105
                _segmentoP += "0";

                //Reservado (uso Banco) ==> 106 - 106
                _segmentoP += " ";

                //Espécie do título ==> 107 – 108
                _segmentoP += Utils.FitStringLength(boleto.EspecieDocumento.Codigo, 2, 2, '0', 0, true, true, true);

                //Identif. de título Aceito/Não Aceito ==> 109 - 109
                _segmentoP += "N";

                //Data da emissão do título ==> 110 - 117
                _segmentoP += boleto.DataDocumento.ToString("ddMMyyyy");

                if (boleto.JurosMora > 0)
                {
                    //Código do juros de mora ==> 118 - 118
                    if (!String.IsNullOrWhiteSpace(boleto
                            .CodJurosMora)) //Possibilita passar o código 2 para JurosMora ao Mes, senão for setado, assume o valor padrão 1 para JurosMora ao Dia
                        _segmentoP += Utils.FitStringLength(boleto.CodJurosMora.ToString(), 1, 1, '0', 0, true, true,
                            true);
                    else
                        _segmentoP += "1";

                    //Data do juros de mora ==> 119 - 126
                    _segmentoP += Utils.FitStringLength(boleto.DataVencimento.ToString("ddMMyyyy"), 8, 8, '0', 0, true,
                        true, false);

                    //Valor da mora/dia ou Taxa mensal ==> 127 - 141
                    _segmentoP += Utils.FitStringLength(boleto.JurosMora.ApenasNumeros(), 15, 15, '0', 0, true, true,
                        true);
                }
                else
                {
                    //Código do juros de mora ==> 118 - 118
                    _segmentoP += "3";

                    //Data do juros de mora ==> 119 - 126
                    _segmentoP += "00000000";

                    //Valor da mora/dia ou Taxa mensal ==> 127 - 141
                    _segmentoP += "000000000000000";
                }

                if (boleto.ValorDesconto > 0)
                {
                    //Código do desconto 1 ==> 142 - 142
                    _segmentoP += "1";

                    //Data de desconto 1 ==> 143 - 150
                    _segmentoP += Utils.FitStringLength(boleto.DataVencimento.ToString("ddMMyyyy"), 8, 8, '0', 0, true,
                        true, false);

                    //Valor ou Percentual do desconto concedido ==> 151 - 165
                    _segmentoP += Utils.FitStringLength(boleto.ValorDesconto.ApenasNumeros(), 15, 15, '0', 0, true,
                        true, true);
                }
                else if (boleto.OutrosDescontos > 0)
                {
                    //Código do desconto 1 ==> 142 - 142
                    _segmentoP += "1";

                    //Data de desconto 1 ==> 143 - 150
                    _segmentoP += Utils.FitStringLength(boleto.DataVencimento.ToString("ddMMyyyy"), 8, 8, '0', 0, true,
                        true, false);

                    //Valor ou Percentual do desconto concedido ==> 151 - 165
                    _segmentoP += Utils.FitStringLength(boleto.OutrosDescontos.ApenasNumeros(), 15, 15, '0', 0, true,
                        true, true);
                }
                else
                {
                    _segmentoP += "0".PadLeft(24, '0');
                }


                //Valor do IOF a ser recolhido ==> 166 - 180
                _segmentoP += "0".PadLeft(15, '0');

                //Valor do abatimento ==> 181 - 195
                _segmentoP += "0".PadLeft(15, '0');

                //Identificação do título na empresa ==> 196 - 220
                _segmentoP += Utils.FitStringLength(boleto.NumeroDocumento, 25, 25, ' ', 0, true, true, false);

                string codigo_protesto = "3";
                string dias_protesto = "00";

                //foreach (var instrucao in boleto.Instrucoes)
                //{
                //    switch ((EnumInstrucoes_Sicredi)instrucao.Codigo)
                //    {
                //        case EnumInstrucoes_Sicredi.PedidoProtesto:
                //            codigo_protesto = "9";
                //            dias_protesto = Utils.FitStringLength(instrucao.QuantidadeDias.ToString(), 2, 2, '0', 0, true, true, true); //Para código '1' – é possível, de 6 a 29 dias
                //            break;
                //        default:
                //            codigo_protesto = "3";
                //            break;
                //    }
                //}

                var instrucaoProtesto = boleto.Instrucoes.FirstOrDefault(c =>
                    (EnumInstrucoes_Sicredi)c.Codigo == EnumInstrucoes_Sicredi.PedidoProtesto);

                if (instrucaoProtesto != null)
                {
                    codigo_protesto = "9";
                    dias_protesto = Utils.FitStringLength(instrucaoProtesto.QuantidadeDias.ToString(), 2, 2, '0', 0,
                        true, true, true); //Para código '1' – é possível, de 6 a 29 dias
                }

                //Código para protesto ==> 221 - 221
                _segmentoP += codigo_protesto;

                //Número de dias para protesto ==> 222 - 223
                _segmentoP += dias_protesto;

                //Código para Baixa/Devolução ==> 224 - 224
                _segmentoP += "1";

                //Número de dias para Baixa/Devolução ==> 225 - 227
                _segmentoP += "060";

                //Código da moeda ==> 228 - 229
                _segmentoP += "09";

                //Reservado (uso Banco) ==> 230 –240
                _segmentoP += " ".PadLeft(11, '0');

                _segmentoP = Utils.SubstituiCaracteresEspeciais(_segmentoP);

                return _segmentoP;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a geração do SEGMENTO P DO DETALHE do arquivo de REMESSA.", ex);
            }
        }

        public override string GerarDetalheSegmentoQRemessa(Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
        {
            try
            {
                string _segmentoQ;

                //Código do Banco na compensação ==> 001 - 003
                _segmentoQ = Utils.FormatCode(Codigo.ToString(), "0", 3, true);

                //Numero do lote remessa ==> 004 - 007
                _segmentoQ += Utils.FitStringLength("1", 4, 4, '0', 0, true, true, true);

                //Tipo de registro ==> 008 - 008
                _segmentoQ += "3";

                //Nº seqüencial do registro no lote ==> 009 - 013
                _segmentoQ += Utils.FitStringLength(numeroRegistro.ToString(), 5, 5, '0', 0, true, true, true);

                //Cód. segmento do registro detalhe ==> 014 - 014
                _segmentoQ += "Q";

                //Reservado (uso Banco) ==> 015 - 015
                _segmentoQ += " ";

                //Código de movimento remessa ==> 016 - 017
                _segmentoQ += ObterCodigoDaOcorrencia(boleto);

                if (boleto.Sacado.CPFCNPJ.Length <= 11)
                    //Tipo de inscrição do sacado ==> 018 - 018
                    _segmentoQ += "1";
                else
                    //Tipo de inscrição do sacado ==> 018 - 018
                    _segmentoQ += "2";

                //Número de inscrição do sacado ==> 019 - 033
                _segmentoQ += Utils.FitStringLength(boleto.Sacado.CPFCNPJ, 15, 15, '0', 0, true, true, true);

                //Nome sacado ==> 034 - 073
                _segmentoQ += Utils
                    .FitStringLength(boleto.Sacado.Nome.TrimStart(' '), 40, 40, ' ', 0, true, true, false).ToUpper();

                //Endereço sacado ==> 074 - 113
                _segmentoQ += Utils.FitStringLength(boleto.Sacado.Endereco.End.TrimStart(' '), 40, 40, ' ', 0, true,
                    true, false).ToUpper();

                //Bairro sacado ==> 114 - 128
                _segmentoQ += Utils.FitStringLength(boleto.Sacado.Endereco.Bairro.TrimStart(' '), 15, 15, ' ', 0, true,
                    true, false).ToUpper();

                //Cep sacado ==> 129 - 133
                _segmentoQ += Utils
                    .FitStringLength(boleto.Sacado.Endereco.CEP.Substring(0, 5), 5, 5, ' ', 0, true, true, false)
                    .ToUpper();

                //Sufixo do Cep do sacado ==> 134 - 136
                _segmentoQ += Utils
                    .FitStringLength(boleto.Sacado.Endereco.CEP.Substring(5, 3), 3, 3, ' ', 0, true, true, false)
                    .ToUpper();

                //Cidade do sacado ==> 137 - 151
                _segmentoQ += Utils.FitStringLength(boleto.Sacado.Endereco.Cidade.TrimStart(' '), 15, 15, ' ', 0, true,
                    true, false).ToUpper();

                //Unidade da federação do sacado ==> 152 - 153
                _segmentoQ += Utils.FitStringLength(boleto.Sacado.Endereco.UF, 2, 2, ' ', 0, true, true, false)
                    .ToUpper();

                //Tipo de inscrição sacador/avalista ==> 154 - 154
                _segmentoQ += "0";

                //Nº de inscrição sacador/avalista ==> 155 - 169
                _segmentoQ += "0".PadLeft(15, '0');

                //Nome do sacador/avalista ==> 170 - 209
                _segmentoQ += " ".PadLeft(40, ' ');

                //Identificador de carne ==> 210 –212
                _segmentoQ += "0".PadLeft(3, '0');

                //Reservado (uso Banco) ==> 213 – 240
                _segmentoQ += " ".PadLeft(28, ' ');

                return Utils.SubstituiCaracteresEspeciais(_segmentoQ);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a geração do SEGMENTO Q DO DETALHE do arquivo de REMESSA.", ex);
            }
        }

        public override string GerarDetalheSegmentoRRemessa(Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
        {
            try
            {
                string _segmentoR;

                //Código do Banco na compensação ==> 001 - 003
                _segmentoR = Utils.FormatCode(Codigo.ToString(), "0", 3, true);

                //Numero do lote remessa ==> 004 - 007
                _segmentoR += Utils.FitStringLength("1", 4, 4, '0', 0, true, true, true);

                //Tipo de registro ==> 008 - 008
                _segmentoR += "3";

                //Nº seqüencial do registro de lote ==> 009 - 013
                _segmentoR += Utils.FitStringLength(numeroRegistro.ToString(), 5, 5, '0', 0, true, true, true);

                //Código segmento do registro detalhe ==> 014 - 014
                _segmentoR += "R";

                //Reservado (uso Banco) ==> 015 - 015
                _segmentoR += " ";

                //Código de movimento ==> 016 - 017
                _segmentoR += ObterCodigoDaOcorrencia(boleto);

                //Implementação do 2 desconto por antecipação ==> 018 - 041
                if (boleto.DataDescontoAntecipacao2.HasValue && boleto.ValorDescontoAntecipacao2.HasValue)
                {
                    _segmentoR += "1" + //'1' = Valor Fixo Até a Data Informada
                                  Utils.FitStringLength(boleto.DataDescontoAntecipacao2.Value.ToString("ddMMyyyy"), 8,
                                      8, '0', 0, true, true, false) +
                                  Utils.FitStringLength(boleto.ValorDescontoAntecipacao2.ApenasNumeros(), 15, 15, '0',
                                      0, true, true, true);
                }
                else
                {
                    // Desconto 2
                    _segmentoR += "000000000000000000000000"; //24 zeros
                }

                //Desconto 3 ==> 042 - 065
                _segmentoR += "000000000000000000000000"; //24 zeros


                if (boleto.PercMulta > 0)
                {
                    //Código da multa ==> 066 - 066
                    _segmentoR += "2";

                    //Data da multa ==> 067 - 074
                    _segmentoR += boleto.DataMulta.ToString("ddMMyyyy");

                    //Valor/Percentual a ser aplicado ==> 075 - 089
                    _segmentoR += Utils.FitStringLength(boleto.PercMulta.ApenasNumeros(), 15, 15, '0', 0, true, true,
                        true);
                }
                else if (boleto.ValorMulta > 0)
                {
                    //Código da multa ==> 066 - 066
                    _segmentoR += "1";

                    //Data da multa ==> 067 - 074
                    _segmentoR += boleto.DataMulta.ToString("ddMMyyyy");

                    //Valor/Percentual a ser aplicado ==> 075 - 089
                    _segmentoR += Utils.FitStringLength(boleto.ValorMulta.ApenasNumeros(), 15, 15, '0', 0, true, true,
                        true);
                }
                else
                {
                    //Código da multa ==> 066 - 066
                    _segmentoR += "0";

                    //Data da multa ==> 067 - 074
                    _segmentoR += "0".PadLeft(8, '0');

                    //Valor/Percentual a ser aplicado ==> 075 - 089
                    _segmentoR += "0".PadLeft(15, '0');
                }

                //Reservado (uso Banco) ==> 090 - 099
                _segmentoR += " ".PadLeft(10, ' ');

                //Mensagem 3 ==> 100 - 139
                _segmentoR += " ".PadLeft(40, ' ');

                //Mensagem 4 ==> 140 - 179
                _segmentoR += " ".PadLeft(40, ' ');

                //Reservado ==> 180 - 240
                _segmentoR += " ".PadLeft(61, ' ');

                _segmentoR = Utils.SubstituiCaracteresEspeciais(_segmentoR);

                return _segmentoR;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a geração do SEGMENTO R DO DETALHE do arquivo de REMESSA.", ex);
            }
        }
        
        public override string GerarTrailerLoteRemessa(int numeroRegistro)
        {
            try
            {
                //Código do Banco na compensação ==> 001 - 003
                string trailer = Utils.FormatCode(Codigo.ToString(), "0", 3, true);

                //Numero do lote remessa ==> 004 - 007
                trailer += Utils.FormatCode("1", "0", 4, true);

                //Tipo de registro ==> 008 - 008
                trailer += "5";

                //Reservado (uso Banco) ==> 009 - 017
                trailer += Utils.FormatCode("", " ", 9);

                //Quantidade de registros do lote ==> 018 - 023
                trailer += Utils.FormatCode(numeroRegistro.ToString(), "0", 6, true);

                //Reservado (uso Banco) ==> 024 - 240
                trailer += Utils.FormatCode("", " ", 217);

                trailer = Utils.SubstituiCaracteresEspeciais(trailer);

                return trailer;
            }
            catch (Exception e)
            {
                throw new Exception("Erro durante a geração do registro TRAILER do LOTE de REMESSA.", e);
            }
        }
        
        public override string GerarTrailerArquivoRemessa(int numeroRegistro)
        {
            try
            {
                //Código do Banco na compensação ==> 001 - 003
                string trailer = Utils.FormatCode(Codigo.ToString(), "0", 3, true);

                //Numero do lote remessa ==> 004 - 007
                trailer += "9999";

                //Tipo de registro ==> 008 - 008
                trailer += "9";

                //Reservado (uso Banco) ==> 009 - 017
                trailer += Utils.FormatCode("", " ", 9);

                //Quantidade de lotes do arquivo ==> 018 - 023
                trailer += Utils.FormatCode("1", "0", 6, true);

                //Quantidade de registros do arquivo ==> 024 - 029
                trailer += Utils.FormatCode(numeroRegistro.ToString(), "0", 6, true);

                //Qtde de contas concil. ==> 030 - 035
                trailer += Utils.FormatCode("", "0", 6);

                //Reservado (uso Banco) ==> 036 - 240
                trailer += Utils.FormatCode("", " ", 205);

                trailer = Utils.SubstituiCaracteresEspeciais(trailer);

                return trailer;
            }
            catch (Exception e)
            {
                throw new Exception("Erro durante a geração do registro TRAILER do ARQUIVO de REMESSA.", e);
            }
        }
    }
}