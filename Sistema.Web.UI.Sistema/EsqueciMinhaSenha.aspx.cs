using System;
using System.Web.Services;
using Sistema.Api.dll.Repositorio.Util;
using Sistema.Api.dll.Src.Seguranca.BE;
using Sistema.Api.dll.Src.Seguranca.VO;

using System.Web;


namespace Sistema.Web.UI.Sistema
{
    public partial class EsqueciMinhaSenha : System.Web.UI.Page
    {
        public int N1 { get; set; }
        public int N2 { get; set; }
        public int CaptchaSound { get; set; }
        public static int[] IntArray { get; set; }

        //Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();

            // Cria um  nome de arquivo mp3 randomico e grava em Sessão
            CaptchaSound = rnd.Next(111111, 999999);

            this.Session["CaptchaSound"] = CaptchaSound;

            IntArray = new int[]
	        {
	            1,2,3,4,5,6,7,8,9
	        };

            N1 = ShuffleArray(IntArray)[0];
            N2 = ShuffleArray(IntArray)[0];
        }

        //ShuffleArray
        public static int[] ShuffleArray(int[] array)
        {
            Random r = new Random();
            for (int i = array.Length; i > 0; i--)
            {
                int j = r.Next(i);
                int k = array[j];
                array[j] = array[i - 1];
                array[i - 1] = k;
            }
            return array;
        }

        //ChecarCaptcha
        public static bool ChecarSomaCaptcha(int n1, int n2, int sum)
        {
            return ((n1 + n2) == sum);
        }

        //ChecarCaptcha
        public static int[] RefazerCaptcha()
        {
            IntArray = new int[]
	        {
	            1,2,3,4,5,6,7,8,9
	        };

            return new int[]
	        {
	            ShuffleArray(IntArray)[0],
                ShuffleArray(IntArray)[0],
	        };
        }

        //RecuperarSenha
        [WebMethod]
        public static string RecuperarSenha( Object inputs)
        {
            var ajax = new Ajax();
            UsuarioBE usuarioBE = null;
            try
            {
                usuarioBE = new UsuarioBE();

                // recupera o texto gerado da imagem captcha
                string imagemCaptcha = HttpContext.Current.Session["CaptchaImageText"].ToString();

                // recupera o captcha informado pelo usuario
                string entradaCaptcha = Convert.ToString(ajax.GetValueObjJson("captcha", inputs));
          
                // Refazer captcha
                ajax.Variante = Json.Serialize(RefazerCaptcha());
                
                // Checagem do captcha para recuperação da senha
                if (imagemCaptcha != entradaCaptcha)
                {
                    throw new Exception("Os dígitos do captcha não confere, por favor tente novamente.");
                }

                // Checagem de login para recuperação de senha
                var emailUsuario = usuarioBE.VerificarUsuario(new UsuarioVO()
                {
                    NomeLogin = Convert.ToString(ajax.GetValueObjJson("login", inputs))
                });

                // Sucesso
                ajax.StatusOperacao = true;
                ajax.AddMessage("Solicitação de recuperação de senha processada com sucesso. Por favor verifique a caixa postal do e-mail: " + emailUsuario + " para novas instruções.", Mensagem.Sucesso);

            }
            catch (Exception ex)
            {
                ajax.StatusOperacao = false;
                ajax.AddMessage(ex.Message, Mensagem.Erro);
            }
            finally
            {
                if (usuarioBE != null)
                    usuarioBE.FecharConexao();
            }

            return ajax.GetAjaxJson();
        }
    }
}