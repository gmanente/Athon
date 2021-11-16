using System.Linq;
using System.Runtime.InteropServices; //Dll Sintegra
using System.Text.RegularExpressions;

namespace Sistema.Api.dll.Repositorio.Validacoes
{
    public class ValidacaoGeral
    {
        //Nesta Parte do código, inserimos a referência a DLL que já está registrada.
        [DllImport("DllInscE32.dll")]
        //Método static extern ConsisteInscricaoEstadual que retornará a validação da IE
        private static extern bool ConsisteInscricaoEstadual(string Inscr, string UF);


        #region NomeValido

        public bool NomeValido(string nome)
        {
            Regex regex = new Regex(@"^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]+$");

            if (regex.IsMatch(nome))
                return true;
            else
                return false;
        }

        #endregion


        #region ContemLetras

        public bool ContemLetras(string letras)
        {
            if (letras.Where(c => char.IsLetter(c)).Count() > 0)
                return true;
            else
                return false;
        }

        #endregion


        #region ContemNumeros

        public bool contemNumeros(string numeros)
        {
            if (numeros.Where(c => char.IsNumber(c)).Count() > 0)
                return true;
            else
                return false;
        }

        #endregion


        #region ContemLetrasEnumeros

        public bool ContemLetrasEnumeros(string texto)
        {
            if (this.ContemLetras(texto) && this.contemNumeros(texto))
                return true;
            else
                return false;
        }

        #endregion


        #region NumeroInteiroValido

        public bool NumeroInteiroValido(string numero)
        {
            Regex rgx = new Regex(@"^[0-9]+$");
            if (rgx.IsMatch(numero))
                return true;
            else
                return false;
        }

        #endregion


        #region NumeroRealValido

        public bool NumeroRealValido(string numeroreal)
        {
            Regex rgx = new Regex(@"^[0-9]+?(.|,[0-9]+)$");
            if (rgx.IsMatch(numeroreal))
                return true;
            else
                return false;
        }

        #endregion


        #region CepValido

        public bool CepValido(string cep)
        {
            Regex rgx = new Regex(@"^\d{5}\-?\d{3}$");
            if (rgx.IsMatch(cep))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion


        #region EmailValido

        public bool EmailValido(string email)
        {
            Regex rgx =
                new Regex(
                    @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
            if (rgx.IsMatch(email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion


        #region UrlValida

        public bool UrlValida(string http)
        {
            Regex rgx = new Regex(@"^((http)|(https)|(ftp)):\/\/([\- \w]+\.)+\w{2,3}(\/ [%\-\w]+(\.\w{2,})?)*$");
            if (rgx.IsMatch(http))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion


        #region IpValido

        public bool IpValido(string ip)
        {
            Regex rgx = new Regex(@"^\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b$");
            if (rgx.IsMatch(ip))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion


        #region DataValida
        public bool DataValida(string data)
        {
            Regex rgx = new Regex(@"^((0[1-9]|[12]\d)\/(0[1-9]|1[0-2])|30\/(0[13-9]|1[0-2])|31\/(0[13578]|1[02]))\/\d{4}$");
            if (rgx.IsMatch(data))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


        #region TelefoneValido
        public bool TelefoneValido(string telefone)
        {
            string patern = "^\\([1-9]{2}\\) (?:[2-8][0-9]|9[1-9])[0-9]{2,3}\\-[0-9]{4}$";

            Regex rgx = new Regex(patern);

            if (rgx.IsMatch(telefone))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


        #region CpfValido
        public bool CpfValido(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");

            if ((cpf.Length != 11) || (cpf == "00000000000") || (cpf == "11111111111") || (cpf == "22222222222") || (cpf == "33333333333") ||
                (cpf == "44444444444") || (cpf == "55555555555") || (cpf == "66666666666") || (cpf == "77777777777") || (cpf == "88888888888") || (cpf == "99999999999"))
            {
                return false;
            }

            for (int j = 0; j < 10; j++)
            {
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;
            }

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }

            int resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();

            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
        #endregion


        #region CnpjValido
        public bool CnpjValido(string cnpj)
        {
            // Se vazio
            if (cnpj.Length == 0)
                return false;
            //Expressao regular que valida cpf
            Regex rgx = new Regex(@"^\d{2}.?\d{3}.?\d{3}/?\d{4}-?\d{2}$");
            if (rgx.IsMatch(cnpj))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


        #region InscEstadualValida
        // Copiar o arquivo  DllInscE32.dll para o diretório c:\windows\System32;
        public bool InscEstadualValida(string pInsc, string pUF)
        {
            // Se vazio
            if (pInsc.Length == 0)
                return false;
            // Limpa caracteres especiais
            pInsc = pInsc.Trim();
            pInsc = pInsc.Replace(".", "").Replace("-", "").Replace("/", "").Replace(" ", "");
            pInsc = pInsc.Replace("+", "").Replace("*", "").Replace(",", "").Replace("?", "");
            pInsc = pInsc.Replace("!", "").Replace("@", "").Replace("#", "").Replace("$", "");
            pInsc = pInsc.Replace("%", "").Replace("¨", "").Replace("&", "").Replace("(", "");
            pInsc = pInsc.Replace("=", "").Replace("[", "").Replace("]", "").Replace(")", "");
            pInsc = pInsc.Replace("{", "").Replace("}", "").Replace(":", "").Replace(";", "");
            pInsc = pInsc.Replace("<", "").Replace(">", "").Replace("ç", "").Replace("Ç", "");
            //Caso a Inscricão e o Estado Estejam Certos, Retorna Verdadeiro
            if (ConsisteInscricaoEstadual(pInsc.Replace(".", "").Replace("-", ""), pUF))
                return false;  // notei que aqui em varios exemplos na internet estava invertido o verdadeiro/falso
            else
                return true;
        }
        #endregion
    }
}