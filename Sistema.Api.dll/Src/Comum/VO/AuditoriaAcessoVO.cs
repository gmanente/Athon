namespace Sistema.Api.dll.Src.Comum.VO
{
    public class AuditoriaAcessoVO : AbstractVO
    {
        public System.DateTime? DataOperacao { get; set; }
        public string NomeUsuario { get; set; }
        public string NomeModulo { get; set; }
        public string NomeSubmodulo { get; set; }
        public string NomeCampus { get; set; }
        public System.DateTime? Data { get; set; }
        public System.DateTime? DataInicial { get; set; }
        public System.DateTime? DataFinal { get; set; }


        private AuditoriaVO auditoria { get; set; }
        public AuditoriaVO Auditoria
        {
            set { auditoria = value; }
            get
            {
                if (auditoria == null && IsInstantiable())
                    auditoria = new AuditoriaVO();

                return auditoria;
            }
        }

    }
}