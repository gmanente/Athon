using System;
using System.Text.RegularExpressions;

namespace Sistema.Api.dll.Repositorio.Validacoes
{
    /// <summary>
    ///  Author......: Michael S. Lopes
    ///  Date........: 23-10-2013
    ///  Description.: Classe resposavel pela validação de catacteres especiais
    /// </summary>
    public class ValidacaoCaracteresEspeciais
    {
        /// <summary>
        /// Author......: Leandro Curioso
        /// Date........: 23-10-2013
        /// </summary>
        /// <param name="texto">O método recebe uma string</param>
        /// <returns>retorna true para válido e false para não válido</returns>
        public static bool IsCaracteresEspeciais(string texto)
        {
            try
            {
                Regex objAlphaPattern = new Regex(@"[^a-zA-Z0-9]");
                bool sts = objAlphaPattern.IsMatch(texto);
                return sts;
            }
            catch (Exception e)
            {

                throw e;
            }


        }
    }
}