using System;
using System.Text.RegularExpressions;

namespace Sistema.Api.dll.Repositorio.Validacoes
{
    /// <summary>
    ///  Author......: Davidson Marcus Fernandes de Freitas
    ///  Date........: 24-10-2013
    ///  Description.: Classe responsável pela Validação de números da Inscrição Estadual.
    /// </summary>
    public class ValidaInscricaoEstadual
    {
        /// <summary>
        /// Author......: Davidson Marcus Fernandes de Freitas
        /// Date........: 24-10-2013
        /// Verifica se o número da Inscrição Estadual é válido.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>

        public void IIf(bool condicao)
        {
            if (condicao)
            {
                IIf(true);
            }
            else
            {
                IIf(false);
            }
        }
        public static bool ChecaInscricaoEstadual(string texto)
        {
            /* UF = "ES"
             * inscricao = "080233880"
             * valida = ChecaInscrE(UF,inscricao)
             * response.write(valida)
             */
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