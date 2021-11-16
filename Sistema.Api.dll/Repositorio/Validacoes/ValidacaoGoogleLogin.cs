using Newtonsoft.Json;
using System;
using System.Net;

namespace Sistema.Api.dll.Repositorio.Validacoes
{
    public class ValidacaoGoogleLogin
    {
        private string name;
        private string email;


        public static ValidacaoGoogleLogin Validar(string accessToken)
        {
            try
            {
                WebClient webClient = new WebClient();

                string response = webClient.DownloadString(string.Format("https://www.googleapis.com/oauth2/v3/tokeninfo?id_token={0}", accessToken));

                if (string.IsNullOrEmpty(response))
                {
                    throw new Exception("A comunicação com o Google falhou. (Não houve resposta do servidor) ");
                }

                var profileResponse = JsonConvert.DeserializeObject<ValidacaoGoogleLogin>(response);


                if (string.IsNullOrEmpty(profileResponse.Email))
                {
                    throw new Exception("A conta do Google não retornou um e-mail válido. (Não houve resposta do servidor) ");
                }


                return profileResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [JsonProperty("name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        [JsonProperty("email")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
    }
}