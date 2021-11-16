using System.Runtime.InteropServices; //Dll Sintegra
using System.Text.RegularExpressions;

namespace Sistema.Api.dll.Repositorio.Validacoes
{
    public class ValidaIERegex
    {
        //Nesta Parte do código, inserimos a referência a DLL que já está registrada.
        [DllImport("DllInscE32.dll")]
        //Método static extern ConsisteInscricaoEstadual que retornará a validação da IE
        public static extern int ConsisteInscricaoEstadual(string Inscr, string UF);

        #region NomeValido

        public bool NomeValido(string nome)
        {
            Regex rgx = new Regex(@"^[aA-zZ]+((\s[aA-zZ]+)+)?$");
            if (rgx.IsMatch(nome))
                return true;
            else
                return false;
        }

        #endregion
    }
}