using System;
using System.Linq;

namespace BoletoNet
{
    public enum EnumEspecieDocumento_BTG
    {
        DuplicataMercantil = 1,
    }

    public class EspecieDocumento_BTG : AbstractEspecieDocumento
    {
        #region Construtores

        public EspecieDocumento_BTG()
        {
        }

        public EspecieDocumento_BTG(EnumEspecieDocumento_BTG especieDocumento)
        {
            try
            {
                this.Carregar(especieDocumento);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao carregar objeto", ex);
            }
        }

        public EspecieDocumento_BTG(string codigo) : this((EnumEspecieDocumento_BTG)int.Parse(codigo))
        {
        }

        #endregion Construtores

        #region Metodos Privados

        private void Carregar(EnumEspecieDocumento_BTG idCodigo)
        {
            try
            {
                this.Banco = new Banco_BTG();
                this.Codigo = ((int)idCodigo).ToString().PadLeft(2, '0');

                switch (idCodigo)
                {
                    case EnumEspecieDocumento_BTG.DuplicataMercantil:
                        this.Especie = "Duplicata Mercantil";
                        this.Sigla = "DM";
                        break;
                    default:
                        this.Codigo = "0";
                        this.Especie = "( Selecione )";
                        this.Sigla = "";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao carregar objeto", ex);
            }
        }

        public static EspeciesDocumento CarregaTodas()
        {
            try
            {
                EspeciesDocumento alEspeciesDocumento = new EspeciesDocumento();

                foreach (EnumEspecieDocumento_BTG val in Enum.GetValues(typeof(EnumEspecieDocumento_BTG)))
                {
                    alEspeciesDocumento.Add(new EspecieDocumento_BTG(val));
                }

                return alEspeciesDocumento;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao listar objetos", ex);
            }
        }

        public override IEspecieDocumento DuplicataMercantil()
        {
            return new EspecieDocumento_BTG(((int)EnumEspecieDocumento_BTG.DuplicataMercantil).ToString());
        }

        public override string getCodigoEspecieBySigla(string sigla)
        {
            throw new NotImplementedException();
        }

        #endregion Metodos Privados
    }
}