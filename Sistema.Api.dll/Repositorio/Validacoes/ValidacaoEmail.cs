using System;
using System.Text.RegularExpressions;

#pragma warning disable 0168 // variable declared but never used.
namespace Sistema.Api.dll.Repositorio.Validacoes
{
    public class ValidacaoEmail
    {
        public static bool IsEmail(string email)
        {
            try
            {

                string textoValidar = email;
                Regex expressaoRegex = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");


                if (expressaoRegex.IsMatch(textoValidar))
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw;
            }

        }

    }
}