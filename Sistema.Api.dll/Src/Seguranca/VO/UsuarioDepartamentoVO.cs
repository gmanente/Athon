using Sistema.Api.dll.Src.Comum.VO;

namespace Sistema.Api.dll.Src.Seguranca.VO
{
    [System.Serializable]
    public class UsuarioDepartamentoVO : AbstractVO
    {
        public bool? Ativar { get; set; }


        public UsuarioVO Usuario { get; set; }
        public DepartamentoVO Departamento { get; set; }


        public UsuarioDepartamentoVO()
        {
            Usuario = new UsuarioVO();
            Departamento = new DepartamentoVO();
        }

    }
}