//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="CodigoMovimento_Santander.cs" company="Néctar Informática Ltda">
//    Boleto.Net
//  </copyright>
//  <summary>
//    Defines the CodigoMovimento_Santander.cs type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace BoletoNet
{
	using System;
	using System.Collections.Generic;
	using Excecoes;

	public enum EnumCodigoMovimento_Santander
	{
		EntradaConfirmada = 02,

		EntradaRejeitada = 03,

		TransferenciaCarteiraEntrada = 04,

		TransferenciaCarteiraBaixa = 05,

		LiquidacaoNormal = 06,

		ConfirmacaoRecebimentoInstrucaoDesconto = 07,

		ConfirmacaoRecebimentoCancelamentoDesconto = 08,

		Baixado = 09,

		BaixadoConformeInstrucoesDaCooperativaDeCredito = 10,

		TituloEmSer = 11,

		AbatimentoConcedido = 12,

		AbatimentoCancelado = 13,

		VencimentoAlterado = 14,

		LiquidacaoEmCartorio = 15,

		LiquidacaoAposBaixa = 17,

		ConfirmacaoDeRecebimentoDeInstrucaoDeProtesto = 19,

		ConfirmacaoDeRecebimentoDeInstrucaoDeSustacaoDeProtesto = 20,

		EntradaDeTituloEmCartorio = 23,

		EntradaRejeitadaPorCEPIrregular = 24,

		ProtestadoEBaixado = 25,

		BaixaRejeitada = 27,

		Tarifa = 28,

		RejeicaoDoPagador = 29,

		AlteracaoRejeitada = 30,

		InstrucaoRejeitada = 32,

		ConfirmacaoDePedidoDeAlteracaoDeOutrosDados = 33,

		RetiradoDeCartorioEManutencaoEmCarteira = 34,

		AceiteDoPagador = 35
	}

	public class CodigoMovimento_Santander : AbstractCodigoMovimento,
											 ICodigoMovimento
	{
		#region Fields

		private readonly Dictionary<EnumCodigoMovimento_Santander, TipoOcorrenciaRetorno> correspondentesFebraban =
			new Dictionary<EnumCodigoMovimento_Santander, TipoOcorrenciaRetorno>()
				{
					{ EnumCodigoMovimento_Santander.EntradaConfirmada, TipoOcorrenciaRetorno.EntradaConfirmada },
					{ EnumCodigoMovimento_Santander.EntradaRejeitada, TipoOcorrenciaRetorno.EntradaRejeitada },
					{ EnumCodigoMovimento_Santander.LiquidacaoNormal, TipoOcorrenciaRetorno.Liquidacao },
					{ EnumCodigoMovimento_Santander.Baixado, TipoOcorrenciaRetorno.Baixa },
					{
						EnumCodigoMovimento_Santander.BaixadoConformeInstrucoesDaCooperativaDeCredito,
						TipoOcorrenciaRetorno.Baixa
					},
					{
						EnumCodigoMovimento_Santander.AbatimentoConcedido,
						TipoOcorrenciaRetorno.ConfirmacaoRecebimentoInstrucaoDeAbatimento
					},
					{
						EnumCodigoMovimento_Santander.AbatimentoCancelado,
						TipoOcorrenciaRetorno.ConfirmacaoRecebimentoInstrucaoDeCancelamentoAbatimento
					},
					{
						EnumCodigoMovimento_Santander.VencimentoAlterado,
						TipoOcorrenciaRetorno.ConfirmacaoRecebimentoInstrucaoAlteracaoDeVencimento
					},
					{
						EnumCodigoMovimento_Santander.LiquidacaoAposBaixa,
						TipoOcorrenciaRetorno.LiquidacaoAposBaixaOuLiquidacaoTituloNaoRegistrado
					},
					{
						EnumCodigoMovimento_Santander.ConfirmacaoDeRecebimentoDeInstrucaoDeProtesto,
						TipoOcorrenciaRetorno.ConfirmacaoRecebimentoInstrucaoDeProtesto
					},
					{
						EnumCodigoMovimento_Santander.ConfirmacaoDeRecebimentoDeInstrucaoDeSustacaoDeProtesto,
						TipoOcorrenciaRetorno.ConfirmacaoRecebimentoInstrucaoDeSustacaoCancelamentoDeProtesto
					},
					{ EnumCodigoMovimento_Santander.EntradaDeTituloEmCartorio, TipoOcorrenciaRetorno.RemessaACartorio },
					{ EnumCodigoMovimento_Santander.Tarifa, TipoOcorrenciaRetorno.DebitoDeTarifasCustas },
					{ EnumCodigoMovimento_Santander.RejeicaoDoPagador, TipoOcorrenciaRetorno.OcorrenciasDoPagador },
					{
						EnumCodigoMovimento_Santander.AlteracaoRejeitada,
						TipoOcorrenciaRetorno.AlteracaoDeDadosRejeitada
					},
					{ EnumCodigoMovimento_Santander.InstrucaoRejeitada, TipoOcorrenciaRetorno.InstrucaoRejeitada },
					{
						EnumCodigoMovimento_Santander.ConfirmacaoDePedidoDeAlteracaoDeOutrosDados,
						TipoOcorrenciaRetorno.ConfirmacaoDaAlteracaoDosDadosDoRateioDeCredito
					},
					{
						EnumCodigoMovimento_Santander.RetiradoDeCartorioEManutencaoEmCarteira,
						TipoOcorrenciaRetorno.ConfirmacaoDoCancelamentoDosDadosDoRateioDeCredito
					},
					{ EnumCodigoMovimento_Santander.TituloEmSer, TipoOcorrenciaRetorno.TitulosEmCarteira },
					{ EnumCodigoMovimento_Santander.ProtestadoEBaixado, TipoOcorrenciaRetorno.ProtestadoEBaixado },
				};

		#endregion

		#region Constructors and Destructors

		public CodigoMovimento_Santander(int codigo)
		{
			try
			{
				this.Carregar(codigo);
			}
			catch (Exception ex)
			{
				throw new Exception("Erro ao carregar objeto", ex);
			}
		}

		#endregion

		#region Public Methods and Operators

		/// <inheritdoc />
		public override TipoOcorrenciaRetorno ObterCorrespondenteFebraban()
		{
			return this.ObterCorrespondenteFebraban(
				this.correspondentesFebraban,
				(EnumCodigoMovimento_Santander)this.Codigo);
		}

		#endregion

		#region Methods

		private void Carregar(int codigo)
		{
			try
			{
				this.Banco = new Banco_Santander();

				var movimento = (EnumCodigoMovimento_Santander)codigo;
				this.Codigo = codigo;
			}
			catch (Exception ex)
			{
				throw new BoletoNetException("Código de movimento é inválido", ex);
			}
		}

		#endregion
	}
}