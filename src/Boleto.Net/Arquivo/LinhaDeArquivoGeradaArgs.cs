using System;
using System.Collections.Generic;
using System.Text;

namespace BoletoNet
{
    public enum EnumTipodeLinha
    {
        HeaderDeArquivo = 1,
        HeaderDeLote = 2,
        DetalheSegmentoP = 3,
        DetalheSegmentoQ = 4,
        DetalheSegmentoR = 5,
        TraillerDeLote = 6,
        TraillerDeArquivo = 7,
        DetalheSegmentoS = 8,
        DetalheSegmentoY = 9
    }

    public class LinhaDeArquivoGeradaArgs : EventArgs
    {
        private readonly string _linha;
        private readonly Boleto _boleto;
        private readonly EnumTipodeLinha _tipoLinha;

        public LinhaDeArquivoGeradaArgs(Boleto boleto, string linha, EnumTipodeLinha tipoLinha)
        {
            try
            {
                _boleto = boleto;
                _linha = linha;
                _tipoLinha = tipoLinha;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao instanciar objeto", ex);
            }
        }       

        public string Linha
        {
            get { return _linha; }
        }

        public Boleto Boleto
        {
            get { return _boleto; }
        }

        public EnumTipodeLinha TipoLinha
        {
            get { return _tipoLinha; }
        }
    }
}
