namespace Sistema.Api.dll.Repositorio.Util
{
    public class Mensagem
    {
        public const string Sucesso = "alert-success";
        public const string Informacao = "alert-info";
        public const string Alerta = "alert-warning";
        public const string Erro = "alert-danger";
        private string CorpoMensagem;

        public static string Show(string mensagem, string tipo)
        {

            string msg = GetMensagem().Replace("%TIPO%", tipo).Replace("%MENSAGEM%", mensagem);
            mensagem = "";
            return msg;
        }

        private static string GetMensagem()
        {
            return "<div class='boxMensagem alert alert-dismissable %TIPO%'>" +
                   "<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;</button>" +
                   "%MENSAGEM%</div>";
        }

        public string ShowAdded()
        {
            string msg = CorpoMensagem;
            return msg;
        }
        public void ClearAdded()
        {
            CorpoMensagem = "";
        }

        public void Add(string mensagem, string tipo)
        {
            CorpoMensagem = CorpoMensagem + GetMensagem().Replace("%TIPO%", tipo).Replace("%MENSAGEM%", mensagem);
        }
    }
    public class MensagemSweetAlert
    {
        public const string Sucesso = "success";
        public const string Erro = "error";
        public const string Alerta = "warning";
    }
}
