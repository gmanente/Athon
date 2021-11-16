using System;
using Sistema.Api.dll.Repositorio.Exceptions;

namespace Sistema.Api.dll.Repositorio.Util
{
    public class ManipulaArquivo
    {
        public static string ArquivoToString(string caminho)
        {
            System.IO.StreamReader objAquivo = null;
            string aquivoString;
            try
            {
                objAquivo = new System.IO.StreamReader(caminho);
                aquivoString = objAquivo.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new SysException("Teste Exceção", ex, 1);
            }
            finally
            {
                if (objAquivo != null)
                {
                    objAquivo.Close();
                }
            }
            return aquivoString;

        }
    }
}