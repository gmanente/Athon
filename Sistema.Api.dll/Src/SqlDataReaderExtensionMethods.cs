using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace Sistema.Api.dll.Src
{
    /// <summary>
    /// Extensão do método SqlDataReader
    /// </summary>
    public static class SqlDataReaderExtensionMethods
    {

        /// <summary>
        /// Este método visa checar se existe determinada coluna em um objeto SqlDataReader
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        private static bool HasColumn(DbDataReader reader, string columnName)
        {
            return Enumerable.Range(0, reader.FieldCount).Any(i => string.Equals(reader.GetName(i), columnName, StringComparison.OrdinalIgnoreCase));
        }
        /// </code>
        /// </example>
        /// <param name="this"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static T GetValue<T>(this SqlDataReader @this, string fieldName)
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            if (!HasColumn(@this, fieldName))
            {
                return default;
            }

            int fieldIndex = @this.GetOrdinal(fieldName);

            if (@this.IsDBNull(fieldIndex))
            {
                return default;
            }

            object value = @this.GetValue(fieldIndex);

            if (value is T variable) return variable;

            try
            {
                //Handling Nullable types i.e, int?, double?, bool? .. etc
                if (Nullable.GetUnderlyingType(typeof(T)) != null)
                {
                    return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(value);
                }

                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception)
            {
                return default;
            }
        }

        /// <summary>
        /// Autor: Giovanni Ramos
        /// Data: 07/06/2018
        /// </summary>
        /// <example>
        /// <code>
        ///     var reader = GetSqlDataReader();
        ///
        ///     while (reader.Read())
        ///     {
        ///         var Pessoa = new PessoaVO();
        ///
        ///         if (reader.IsNotNull("PessoaNome"))
        ///             Pessoa.Nome = reader.GetValue<string>("PessoaNome");
        ///         }
        ///     }
        /// </code>
        /// </example>
        /// <param name="this"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static bool IsNotNull(this SqlDataReader @this, string fieldName)
        {
            if (@this == null)
            {
                throw new ArgumentNullException("this");
            }

            try
            {
                return !@this.IsDBNull(@this.GetOrdinal(fieldName));
            }
            catch (Exception)
            {
                throw new Exception(string.Format("A coluna <b>{0}</b> não existe na consulta.", fieldName));
            }
        }

    }
}