using System;
using System.Collections.Generic;
using System.Text;

namespace BoletoNet
{
    public enum EnumTipodeLinhaLida
    {
        HeaderDeArquivo = 1,
        HeaderDeLote = 2,
        DetalheSegmentoT = 3,
        DetalheSegmentoU = 4,
        TraillerDeLote = 5,
        TraillerDeArquivo = 6,
        DetalheSegmentoW = 7,
        DetalheSegmentoE = 8,
        DetalheSegmentoY = 9
    }

    public class LinhaDeArquivoLidaArgs : EventArgs
    {
        private readonly string _linha;
        private readonly object _detalhe;
        private readonly EnumTipodeLinhaLida _tipoLinha;

        public LinhaDeArquivoLidaArgs(object detalhe, string linha)
        {
            try
            {
                _linha = linha;
                _detalhe = detalhe;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao instanciar objeto", ex);
            }
        }

        public LinhaDeArquivoLidaArgs(object detalhe, string linha, EnumTipodeLinhaLida tipoLinha)
        {
            try
            {
                _linha = linha;
                _detalhe = detalhe;
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

        public object Detalhe
        {
            get { return _detalhe; }
        }

        public EnumTipodeLinhaLida TipoLinha
        {
            get { return _tipoLinha; }
        }
    }
}
