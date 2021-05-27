﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BoletoNet
{
    #region Enumerado

    public enum EnumEspecieDocumento_Sicredi
    {
        DuplicataMercantilIndicacao,
        DuplicataRural,
        NotaPromissoria,
        NotaPromissoriaRural,
        NotaSeguros,
        Recibo,
        LetraCambio,
        NotaDebito,
        DuplicataServicoIndicacao,
        Outros,
    }

    #endregion 
    
    public class EspecieDocumento_Sicredi : AbstractEspecieDocumento, IEspecieDocumento
    {
        #region Construtores

        public EspecieDocumento_Sicredi()
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

        public EspecieDocumento_Sicredi(string codigo)
        {
            try
            {
                this.carregar(codigo);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

        #endregion

         public string getCodigoEspecieByEnum(EnumEspecieDocumento_Sicredi especie)
        {
            switch (especie)
            {
                case EnumEspecieDocumento_Sicredi.DuplicataMercantilIndicacao: return "03";
                case EnumEspecieDocumento_Sicredi.DuplicataRural:return "06";
                case EnumEspecieDocumento_Sicredi.NotaPromissoria: return "12";
                case EnumEspecieDocumento_Sicredi.NotaPromissoriaRural: return "13";
                case EnumEspecieDocumento_Sicredi.NotaSeguros: return "16";
                case EnumEspecieDocumento_Sicredi.Recibo: return "17";
                case EnumEspecieDocumento_Sicredi.LetraCambio: return "07";
                case EnumEspecieDocumento_Sicredi.NotaDebito: return "19";
                case EnumEspecieDocumento_Sicredi.DuplicataServicoIndicacao: return "05";
                case EnumEspecieDocumento_Sicredi.Outros: return "99";
                default: return "99";

            }
        }

        public static EnumEspecieDocumento_Sicredi getEnumEspecieByCodigo(string codigo)
        {
            switch (codigo)
            {
                case "03": return  EnumEspecieDocumento_Sicredi.DuplicataMercantilIndicacao;
                case "06": return  EnumEspecieDocumento_Sicredi.DuplicataRural;
                case "12": return  EnumEspecieDocumento_Sicredi.NotaPromissoria;
                case "13": return EnumEspecieDocumento_Sicredi.NotaPromissoriaRural;
                case "16": return EnumEspecieDocumento_Sicredi.NotaSeguros;
                case "17": return EnumEspecieDocumento_Sicredi.Recibo;
                case "07": return EnumEspecieDocumento_Sicredi.LetraCambio;
                case "19": return EnumEspecieDocumento_Sicredi.NotaDebito;
                case "05": return EnumEspecieDocumento_Sicredi.DuplicataServicoIndicacao;
                case "99": return EnumEspecieDocumento_Sicredi.Outros;
                default: return EnumEspecieDocumento_Sicredi.Outros;
            }
        }

        public override string getCodigoEspecieBySigla(string sigla)
        {
            switch (sigla)
            {
                case "DMI": return "A";
                case "DR": return "B";
                case "NP": return "C";
                case "NPR": return "D";
                case "NS": return "E";
                case "RC": return "G";
                case "LC": return "H";
                case "ND": return "I";
                case "DSI": return "J";
                case "OS": return "K";
                default: return "K";
            }
        }

        #region Metodos Privados

        private void carregar(string idCodigo)
        {
            try
            {
                this.Banco = new Banco_Sicredi();

                switch (getEnumEspecieByCodigo(idCodigo))
                {
                    case EnumEspecieDocumento_Sicredi.DuplicataMercantilIndicacao:
                        this.Codigo = getCodigoEspecieByEnum(EnumEspecieDocumento_Sicredi.DuplicataMercantilIndicacao);
                        this.Especie = "Duplicata Mercantil p/ Indicação";
                        this.Sigla = "DMI";
                        break;
                        case EnumEspecieDocumento_Sicredi.DuplicataRural:
                        this.Codigo = getCodigoEspecieByEnum(EnumEspecieDocumento_Sicredi.DuplicataRural);
                        this.Especie = "Duplicata Rural";
                        this.Sigla = "DR";
                        break;
                        case EnumEspecieDocumento_Sicredi.NotaPromissoria:
                        this.Codigo = getCodigoEspecieByEnum(EnumEspecieDocumento_Sicredi.NotaPromissoria);
                        this.Especie = "Nota Promissória";
                        this.Sigla = "NP";
                        break;
                        case EnumEspecieDocumento_Sicredi.NotaPromissoriaRural:
                        this.Codigo = getCodigoEspecieByEnum(EnumEspecieDocumento_Sicredi.NotaPromissoriaRural);
                        this.Especie = "Nota Promissória Rural";
                        this.Sigla = "NR";
                        break;
                        case EnumEspecieDocumento_Sicredi.NotaSeguros:
                        this.Codigo = getCodigoEspecieByEnum(EnumEspecieDocumento_Sicredi.NotaSeguros);
                        this.Especie = "Nota de Seguros";
                        this.Sigla = "NS";
                        break;
                        case EnumEspecieDocumento_Sicredi.Recibo:
                        this.Codigo = getCodigoEspecieByEnum(EnumEspecieDocumento_Sicredi.Recibo);
                        this.Especie = "Recibo";
                        this.Sigla = "RC";
                        break;
                        case EnumEspecieDocumento_Sicredi.LetraCambio:
                        this.Codigo = getCodigoEspecieByEnum(EnumEspecieDocumento_Sicredi.LetraCambio);
                        this.Especie = "Letra de Câmbio";
                        this.Sigla = "LC";
                        break;
                        case EnumEspecieDocumento_Sicredi.NotaDebito:
                        this.Codigo = getCodigoEspecieByEnum(EnumEspecieDocumento_Sicredi.NotaDebito);
                        this.Especie = "Nota de Débito";
                        this.Sigla = "ND";
                        break;
                        case EnumEspecieDocumento_Sicredi.DuplicataServicoIndicacao:
                        this.Codigo = getCodigoEspecieByEnum(EnumEspecieDocumento_Sicredi.DuplicataServicoIndicacao);
                        this.Especie = "Duplicata de Serviço p/ Indicação";
                        this.Sigla = "DSI";
                        break;
                        case EnumEspecieDocumento_Sicredi.Outros:
                        this.Codigo = getCodigoEspecieByEnum(EnumEspecieDocumento_Sicredi.Outros);
                        this.Especie = "Outros";
                        this.Sigla = "OS";
                        break;
                    default:
                        this.Codigo = "0";
                        this.Especie = "( Selecione )";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

        public static EspeciesDocumento CarregaTodas()
        {
            EspeciesDocumento especiesDocumento = new EspeciesDocumento();
            EspecieDocumento_Sicredi ed = new EspecieDocumento_Sicredi();

            foreach (EnumEspecieDocumento_Sicredi item in Enum.GetValues(typeof(EnumEspecieDocumento_Sicredi)))
                especiesDocumento.Add(new EspecieDocumento_Sicredi(ed.getCodigoEspecieByEnum(item)));

            return especiesDocumento;
        }

        public override IEspecieDocumento DuplicataMercantil()
        {
            return new EspecieDocumento_Sicredi(getCodigoEspecieByEnum(EnumEspecieDocumento_Sicredi.DuplicataMercantilIndicacao));
        }

        #endregion
    }
}
