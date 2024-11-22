using BoletoNet.Enums;
using System;
using System.Collections.Generic;
using System.IO;

namespace BoletoNet
{
    public class ArquivoRetornoCNAB400 : AbstractArquivoRetorno, IArquivoRetorno
    {

        private HeaderRetorno _headerRetorno = new HeaderRetorno();
        private List<DetalheRetorno> _listaDetalhe = new List<DetalheRetorno>();

        public List<DetalheRetorno> ListaDetalhe
        {
            get { return _listaDetalhe; }
            set { _listaDetalhe = value; }
        }

        public HeaderRetorno HeaderRetorno
        {
            get { return _headerRetorno; }
            set { _headerRetorno = value; }
        }

        #region Construtores

        public ArquivoRetornoCNAB400()
        {
            this.TipoArquivo = TipoArquivo.CNAB400;
        }

        #endregion

        #region Métodos de instância

        public override void LerArquivoRetorno(IBanco banco, Stream arquivo)
        {
            try
            {
                var stream = new StreamReader(arquivo, System.Text.Encoding.UTF8);
                string linha;

                while ((linha = stream.ReadLine()) != null)
                {
                    var segmento = DetalheRetorno.PrimeiroCaracter(linha);

                    switch (segmento)
                    {
                        case "0":
                            {
                                this.HeaderRetorno = banco.LerHeaderRetornoCNAB400(linha);
                                break;
                            }

                        case "1":
                        case "7" when banco.Codigo == (int)Bancos.CECRED || banco.Codigo == (int)Bancos.BancoBrasil:
                            {
                                //// 85 - CECRED - Código de registro detalhe 7 para CECRED
                                //// 1 - Banco do Brasil- Código de registro detalhe 7 para convênios com 7 posições, e detalhe 1 para convênios com 6 posições (colocado as duas, pois não interferem em cada tipo de arquivo)
                                var detalhe = banco.LerDetalheRetornoCNAB400(linha);
                                this.ListaDetalhe.Add(detalhe);
                                this.OnLinhaLida(detalhe, linha);

                                break;
                            }
                    }
                }

                //// TODO: Tratar Triller.
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
