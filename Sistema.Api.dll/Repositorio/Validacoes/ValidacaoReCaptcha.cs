using Newtonsoft.Json;
using Sistema.Api.dll.Src;
using System;
using System.Collections.Generic;
using System.Net;

namespace Sistema.Api.dll.Repositorio.Validacoes
{
    public class ValidacaoReCaptcha
    {
        private static string ambiente = Dominio.AppState.ToString();

        public static string siteKey = ambiente == "Producao" ? "6LdlDDgUAAAAAHTBpAgHyZ2C1wKy7AO02FXHyZof" : "6LeIxAcTAAAAAJcZVRqyHh71UMIEGNQ_MXjiZKhI"; // padrão de teste
        private static string secretKey = ambiente == "Producao" ? "6LdlDDgUAAAAAGiNtcYIEh9Ve4nCT-W4KOsHb9ET" : "6LeIxAcTAAAAAGG-vFI1TnRWxMZNFuojJ4WifJWe"; // padrão de teste
        private bool success;
        private List<string> errorCodes;


        public static bool Validar(string codigoReCaptchaResolvido)
        {
            try
            {
                WebClient webClient = new WebClient();

                string response = webClient.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, codigoReCaptchaResolvido));

                if (string.IsNullOrEmpty(response))
                {
                    throw new Exception("A comunicação com o Google ReCaptcha falhou. (Não houve resposta do servidor) ");
                }

                var captchaResponse = JsonConvert.DeserializeObject<ValidacaoReCaptcha>(response);

                return captchaResponse.Success;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [JsonProperty("success")]
        public bool Success
        {
            get { return success; }
            set { success = value; }
        }


        [JsonProperty("error-codes")]
        public List<string> ErrorCodes
        {
            get { return errorCodes; }
            set { errorCodes = value; }
        }
    }
}