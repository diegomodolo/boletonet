using System;
using System.Collections;
using System.Text;

namespace BoletoNet
{
    #region Enumerado

    public enum EnumInstrucoes_Sicredi
    {
        CadastroDeTitulo = 1,                      
        PedidoBaixa = 2,                  
        ConcessaoAbatimento = 4,
        CancelamentoAbatimentoConcedido = 5,
        AlteracaoVencimento = 6,
        PedidoProtesto = 9,
        SustarProtestoBaixarTitulo = 18,
        SustarProtestoManterCarteira = 19,
        AlteracaoOutrosDados = 31,
        AlteracaoOutrosDados_Desconto = 311,
        AlteracaoOutrosDados_JuroDia = 312,
        AlteracaoOutrosDados_DescontoAntecipacao = 313,
        AlteracaoOutrosDados_DataLimiteDesconto = 314,
        AlteracaoOutrosDados_CancelamentoProtestoAutomatico = 315,
        //AlteracaoOutrosDados_CarteiraDeCobranca = 316,  não disponivel...


        OutrasInstrucoes_ExibeMensagem_DescontoPontualidade = 899,
        OutrasInstrucoes_ExibeMensagem_MoraDiaria = 900,
        OutrasInstrucoes_ExibeMensagem_MultaVencimento = 901
    }

    #endregion 

    public class Instrucao_Sicredi: AbstractInstrucao, IInstrucao
    {

        #region Construtores 

		public Instrucao_Sicredi()
		{
			try
			{
                this.Banco = new Banco(748);
			}
			catch (Exception ex)
			{
                throw new Exception("Erro ao carregar objeto", ex);
			}
		}

        public Instrucao_Sicredi(int codigo)
        {
            this.carregar(codigo, 0);
        }

        public Instrucao_Sicredi(int codigo, int nrDias)
        {
            this.carregar(codigo, nrDias);
        }
        public Instrucao_Sicredi(int codigo, double valor)
        {
            this.carregar(codigo, valor);
        }

        public Instrucao_Sicredi(int codigo, double valor, EnumTipoValor tipoValor)
        {
            this.carregar(codigo, valor, tipoValor);
        }
        #endregion

        #region Metodos Privados


        private void carregar(int idInstrucao, double valor, EnumTipoValor tipoValor = EnumTipoValor.Percentual)
        {
            try
            {
                this.Banco = new Banco_Sicredi();
                this.Valida();

                switch ((EnumInstrucoes_Sicredi)idInstrucao)
                {
                    case EnumInstrucoes_Sicredi.OutrasInstrucoes_ExibeMensagem_MoraDiaria:
                        this.Codigo = (int)EnumInstrucoes_Sicredi.AlteracaoOutrosDados;
                        this.Descricao = String.Format("  - APÓS VENCIMENTO COBRAR JUROS DE {0} {1} POR DIA DE ATRASO",
                            (tipoValor.Equals(EnumTipoValor.Reais) ? "R$ " : valor.ToString("F3")),
                            (tipoValor.Equals(EnumTipoValor.Percentual) ? "%" : valor.ToString("F2")));
                        break;
                    case EnumInstrucoes_Sicredi.OutrasInstrucoes_ExibeMensagem_MultaVencimento:
                        this.Codigo = (int)EnumInstrucoes_Sicredi.AlteracaoOutrosDados;
                        this.Descricao = String.Format("  - APÓS VENCIMENTO COBRAR MULTA DE {0} {1}",
                            (tipoValor.Equals(EnumTipoValor.Reais) ? "R$ " : valor.ToString("F2")),
                            (tipoValor.Equals(EnumTipoValor.Percentual) ? "%" : valor.ToString("F2")));
                        break;
                    case EnumInstrucoes_Sicredi.AlteracaoOutrosDados_Desconto:
                        this.Codigo = (int)EnumInstrucoes_Sicredi.AlteracaoOutrosDados;
                        this.Descricao = "  - CONCEDER DESCONTO DE R$ " + valor;
                        break;
                    case EnumInstrucoes_Sicredi.AlteracaoOutrosDados_DescontoAntecipacao:
                        this.Codigo = (int)EnumInstrucoes_Sicredi.AlteracaoOutrosDados;
                        this.Descricao = "  - CONCEDER DESCONTO DE R$ " + valor + "POR DIA DE ANTECIPAÇÃO";
                        break;
                    case EnumInstrucoes_Sicredi.AlteracaoOutrosDados_JuroDia:
                        this.Codigo = (int)EnumInstrucoes_Sicredi.AlteracaoOutrosDados;
                        this.Descricao = "  - APÓS VENCIMENTO COBRAR JURO DE " + valor + "% POR DIA DE ATRASO";
                        break;

                    case EnumInstrucoes_Sicredi.OutrasInstrucoes_ExibeMensagem_DescontoPontualidade:
                        this.Codigo = (int)EnumInstrucoes_Sicredi.OutrasInstrucoes_ExibeMensagem_DescontoPontualidade;
                        this.Descricao = " - ATÉ O VENCIMENTO CONCEDER DESCONTO DE R$" + valor;
                        break;

                    default:
                        this.Codigo = 0;
                        this.Descricao = " (Selecione) ";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

        private void carregar(int idInstrucao, int nrDias)
        {
            try
            {
                this.Banco = new Banco_Sicredi();
                this.Valida();

                switch ((EnumInstrucoes_Sicredi)idInstrucao)
                {
                    case EnumInstrucoes_Sicredi.CadastroDeTitulo:
                        this.Codigo = (int)EnumInstrucoes_Sicredi.CadastroDeTitulo;
                        this.Descricao = "";
                        break;
                    case EnumInstrucoes_Sicredi.PedidoBaixa:
                        this.Codigo = (int)EnumInstrucoes_Sicredi.PedidoBaixa;
                        this.Descricao = "";
                        break;   
                    case EnumInstrucoes_Sicredi.ConcessaoAbatimento:
                        this.Codigo = (int)EnumInstrucoes_Sicredi.ConcessaoAbatimento;
                        this.Descricao = "";
                        break;
                    case EnumInstrucoes_Sicredi.CancelamentoAbatimentoConcedido:
                        this.Codigo = (int)EnumInstrucoes_Sicredi.CancelamentoAbatimentoConcedido;
                        this.Descricao = "";
                        break;
                    case EnumInstrucoes_Sicredi.AlteracaoVencimento:
                        this.Codigo = (int)EnumInstrucoes_Sicredi.AlteracaoVencimento;
                        this.Descricao = "";
                        break;
                    case EnumInstrucoes_Sicredi.PedidoProtesto:
                        this.Codigo = (int)EnumInstrucoes_Sicredi.PedidoProtesto;
                        this.Descricao = "  - PROTESTAR APÓS " + nrDias + " DIAS ÚTEIS DO VENCIMENTO";
                        break;
                    case EnumInstrucoes_Sicredi.SustarProtestoBaixarTitulo:
                        this.Codigo = (int)EnumInstrucoes_Sicredi.SustarProtestoBaixarTitulo;
                        this.Descricao = "";
                        break;
                    case EnumInstrucoes_Sicredi.SustarProtestoManterCarteira:
                        this.Codigo = (int)EnumInstrucoes_Sicredi.SustarProtestoManterCarteira;
                        this.Descricao = "";
                        break;
                    case EnumInstrucoes_Sicredi.AlteracaoOutrosDados:
                        this.Codigo = (int)EnumInstrucoes_Sicredi.AlteracaoOutrosDados;
                        this.Descricao = "";
                        break;
                    case EnumInstrucoes_Sicredi.OutrasInstrucoes_ExibeMensagem_DescontoPontualidade:
                        this.Codigo = (int)EnumInstrucoes_Sicredi.OutrasInstrucoes_ExibeMensagem_DescontoPontualidade;
                        this.Descricao = "";
                        break;

                    default:
                        this.Codigo = 0;
                        this.Descricao = " (Selecione) ";
                        break;
                }

                this.QuantidadeDias = nrDias;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }


        public override void Valida()
        {
            //base.Valida();
        }

        #endregion

    }
}
