using System;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.DAO
{
    /// <summary>
    /// Extensão do método SqlCommand Param
    /// </summary>
    public static class ExtensionMethods
    {

        /// <summary>
        /// Autores: Carlos Cortez & Jeferson Bassalobre
        /// Data: 30/11/2016
        /// Descrição: Adiciona o parametro de qualquer tipo de dados que não seja string que aceita null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parms"></param>
        /// <param name="parameterName"></param>
        /// <param name="nullable"></param>
        /// <returns></returns>
        public static SqlParameter AddWithNullable<T>(this SqlParameterCollection parms, string parameterName, T? nullable) where T : struct
        {
            if (nullable.HasValue)
                return parms.AddWithValue(parameterName, nullable.Value);
            else
                return parms.AddWithValue(parameterName, DBNull.Value);
        }

        /// <summary>
        /// Autores: Carlos Cortez & Jeferson Bassalobre
        /// Data: 30/11/2016
        /// Descrição: Adiciona o parametro de qualquer tipo de dados que não seja string que não aceita null, metodo add normal do sqlcommand param
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parms"></param>
        /// <param name="parameterName"></param>
        /// <param name="nullable"></param>
        /// <returns></returns>
        public static SqlParameter AddWithNullable<T>(this SqlParameterCollection parms, string parameterName, T nullable) where T : struct
        {
            return parms.AddWithValue(parameterName, nullable);
        }

        /// <summary>
        /// Autores: Carlos Cortez & Jeferson Bassalobre
        /// Data: 30/11/2016
        /// Descrição: Adiciona o parametro do tipo de dados string
        /// </summary>
        /// <param name="parms"></param>
        /// <param name="parameterName"></param>
        /// <param name="nullable"></param>
        /// <returns></returns>
        public static SqlParameter AddWithNullable(this SqlParameterCollection parms, string parameterName, string nullable)
        {
            if (!string.IsNullOrEmpty(nullable))
                return parms.AddWithValue(parameterName, nullable);
            else
                return parms.AddWithValue(parameterName, DBNull.Value);
        }

    }
}
