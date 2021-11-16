using System;
using System.Text.RegularExpressions;

namespace Sistema.Api.dll.Repositorio.Validacoes
{
    /// <summary>
    ///  Author......: Michael S. Lopes
    ///  Date........: 23-10-2013
    ///  Description.: Classe resposavel pela validação nome sem número
    /// </summary>
    public class ValidacaoNomeSemNumero
    {
        /// <summary>
        /// Author......: Leandro Curioso
        /// Date........: 23-10-2013
        /// </summary>
        /// <param name="texto">O método recebe uma string</param>
        /// <returns>retorna true para válido e false para não válido</returns>
        public static bool IsNomeSemNumero(string texto)
        {
            try
            {
                Regex objAlphaPattern = new Regex(@"^[A-Za-zÀ-ú']+$");
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