using System;
using BoletoNet.Excecoes;

namespace BoletoNet
{
    public class CodigoMovimento_BTG: AbstractCodigoMovimento, ICodigoMovimento
    {
        public CodigoMovimento_BTG(int codigoMovimento)
        {
            this.Carregar(codigoMovimento);
        }

        private void Carregar(int codigoMovimento)
        {
            try
            {
                this.Banco = new Banco_Santander();

                this.Codigo = codigoMovimento;
            }
            catch (Exception ex)
            {
                throw new BoletoNetException("Código de movimento é inválido", ex);
            }
        }

        public override TipoOcorrenciaRetorno ObterCorrespondenteFebraban()
        {
            throw new System.NotImplementedException();
        }
    }
}