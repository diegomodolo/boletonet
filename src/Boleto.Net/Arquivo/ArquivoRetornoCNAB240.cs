using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace BoletoNet
{
    public class ArquivoRetornoCNAB240 : AbstractArquivoRetorno, IArquivoRetorno
    {
        private readonly Stream _streamArquivo;
        //private string _caminhoArquivo;
        private List<DetalheRetornoCNAB240> _listaDetalhes = new List<DetalheRetornoCNAB240>();

        #region Propriedades
        //public string CaminhoArquivo
        //{
        //    get { return _caminhoArquivo; }
        //}
        public Stream StreamArquivo
        {
            get { return _streamArquivo; }
        }
        public List<DetalheRetornoCNAB240> ListaDetalhes
        {
            get { return _listaDetalhes; }
            set { _listaDetalhes = value; }
        }
        #endregion Propriedades

        #region Construtores

        public ArquivoRetornoCNAB240()
        {
            this.TipoArquivo = TipoArquivo.CNAB240;
        }

        public ArquivoRetornoCNAB240(Stream streamArquivo)
        {
            this.TipoArquivo = TipoArquivo.CNAB240;
            _streamArquivo = streamArquivo;
        }

        public ArquivoRetornoCNAB240(string caminhoArquivo)
        {
            this.TipoArquivo = TipoArquivo.CNAB240;

            _streamArquivo = new StreamReader(caminhoArquivo).BaseStream;
        }
        #endregion

        #region Métodos de instância

        public void LerArquivoRetorno(IBanco banco)
        {
            LerArquivoRetorno(banco, StreamArquivo);
        }

        public override void LerArquivoRetorno(IBanco banco, Stream arquivo)
        {
            try
            {
                StreamReader stream = new StreamReader(arquivo, System.Text.Encoding.UTF8);
                string linha = "";

                DetalheRetornoCNAB240 detalheAnterior = null;

                string numeroRemessa = string.Empty;

                while ((linha = stream.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(linha))
                    {
                        var detalheRetorno = new DetalheRetornoCNAB240();

                        switch (linha.Substring(7, 1))
                        {
                            case "0": //Header de arquivo
                                OnLinhaLida(null, linha, EnumTipodeLinhaLida.HeaderDeArquivo);
                                detalheRetorno.HeaderArquivo.LerHeaderDeArquivoCNAB240(linha);
                                numeroRemessa = detalheRetorno.HeaderArquivo.NumeroRemessa;
                                break;
                            case "1": //Header de lote
                                OnLinhaLida(null, linha, EnumTipodeLinhaLida.HeaderDeLote);
                                this.DetalheRetorno = new DetalheRetorno();
                                this.DetalheRetorno.CodigoBanco = int.TryParse(linha.Substring(0, 3), out var codigoBanco) ? codigoBanco : banco.Codigo;
                                this.DetalheRetorno.NumeroSequencial = int.TryParse(linha.Substring(183, 8), out var sequencialHeaderLote) ? sequencialHeaderLote : 0;
                                if (!string.IsNullOrWhiteSpace(numeroRemessa) && (this.DetalheRetorno.NumeroSequencial == 0 || banco.Codigo == 748))
                                    this.DetalheRetorno.NumeroSequencial = int.TryParse(numeroRemessa, out var numeroRemessaInt) ? numeroRemessaInt : 0;
                                break;
                            case "3": //Detalhe
                                if (linha.Substring(13, 1) == "W")
                                {
                                    OnLinhaLida(detalheRetorno, linha, EnumTipodeLinhaLida.DetalheSegmentoW);
                                    detalheRetorno.SegmentoW.LerDetalheSegmentoWRetornoCNAB240(linha);
                                }
                                else if (linha.Substring(13, 1) == "E")
                                {
                                    OnLinhaLida(detalheRetorno, linha, EnumTipodeLinhaLida.DetalheSegmentoE);
                                    detalheRetorno.SegmentoE = new DetalheSegmentoERetornoCNAB240();
                                    detalheRetorno.SegmentoE.LerDetalheSegmentoERetornoCNAB240(linha);
                                }
                                else if (linha.Substring(13, 1) == "T")
                                {
                                    //Irá ler o Segmento T e em sequencia o Segmento U
                                    detalheRetorno.SegmentoT = banco.LerDetalheSegmentoTRetornoCNAB240(linha);
                                    linha = stream.ReadLine();
                                    detalheRetorno.SegmentoU = banco.LerDetalheSegmentoURetornoCNAB240(linha);

                                    OnLinhaLida(detalheRetorno, linha, EnumTipodeLinhaLida.DetalheSegmentoU);
                                }
                                // as linhas abaixo foram comentadas a pedido do Bruno/Gabriel para atender cenário do cliente
                                // Conceito Rações
                                // else if (linha.Substring(13, 1) == "Y")
                                // {
                                //     detalheRetorno.SegmentoY = banco.LerDetalheSegmentoYRetornoCNAB240(linha);
                                //
                                //     if (detalheAnterior != null)
                                //     {
                                //         detalheAnterior.SegmentoY = detalheRetorno.SegmentoY;
                                //     }
                                //
                                //     OnLinhaLida(detalheRetorno, linha, EnumTipodeLinhaLida.DetalheSegmentoY);
                                // }

                                detalheAnterior = detalheRetorno;

                                ListaDetalhes.Add(detalheRetorno);
                                break;
                            case "5": //Trailler de lote
                                OnLinhaLida(null, linha, EnumTipodeLinhaLida.TraillerDeLote);
                                break;
                            case "9": //Trailler de arquivo
                                OnLinhaLida(null, linha, EnumTipodeLinhaLida.TraillerDeArquivo);
                                break;
                        }

                    }
                }

                stream.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao ler arquivo.", ex);
            }
        }

        #endregion
    }
}
