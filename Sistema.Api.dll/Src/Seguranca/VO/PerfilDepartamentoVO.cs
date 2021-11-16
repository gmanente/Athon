using Sistema.Api.dll.Src.Comum.VO;

namespace Sistema.Api.dll.Src.Seguranca.VO
{
    public class PerfilDepartamentoVO : AbstractVO
    {
        public bool? Ativar { get; set; }


        public PerfilVO Perfil { get; set; }
        public DepartamentoVO Departamento { get; set; }


        public PerfilDepartamentoVO()
        {
            Perfil = new PerfilVO();
            Departamento = new DepartamentoVO();
        }

    }
}