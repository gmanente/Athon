namespace Sistema.Api.dll.Src.Comum.VO
{
    [System.Serializable]
    // ESTA CLASSE NÃO PODE SER ALTERADA PARA NÃO IMPACTAR NO LOG
    public class ParametroCampusVO : AbstractVO
    {
        public long IdCampus { get; set; }
        public long IdUsuario { get; set; }
        public System.DateTime? DataCadastro { get; set; }
        public string Valor { get; set; }
        public bool? Ativo { get; set; }


        private ParametroVO parametro { get; set; }
        public ParametroVO Parametro
        {
            set
            {
                parametro = value;
            }

            get
            {
                if (parametro == null && IsInstantiable())
                    parametro = new ParametroVO();

                return parametro;
            }
        }

        public ParametroCampusVO()
        {
            NomeTabela = "DBAthon.dbo.ParametroCampus";
        }

    }
}