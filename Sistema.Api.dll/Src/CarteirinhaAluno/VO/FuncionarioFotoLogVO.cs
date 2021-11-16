using Sistema.Api.dll.Src.Seguranca.VO;

namespace Sistema.Api.dll.Src.CarteirinhaAluno.VO
{
    public class FuncionarioFotoLogVO
    {
        public FuncionarioFotoVO FuncionarioFoto { get; set; }
        public UsuarioVO Usuario { get; set; }
        public System.DateTime? DataImpressao { get; set; }


        public FuncionarioFotoLogVO()
        {
            FuncionarioFoto = new FuncionarioFotoVO();
            Usuario = new UsuarioVO();
        }

    }
}