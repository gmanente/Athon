using System;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src
{
    /// <summary>
    ///  Author......: Michael S. Lopes , Leandro Curioso
    ///  Date........: 08-01-2014
    ///  Description.: Classe resposavel pela abstração Be a qual toda classe
    /// referente a esse domínio deverão herdar.
    /// </summary>
    public abstract class AbstractBE : SqlServerConnection
    {
        protected AbstractBE()
            : base(Dominio.Conexoes.Comum)
        {
            //"SSPI"
        }

        protected AbstractBE(SqlCommand sqlComm)
            : base(sqlComm)
        {}


        /// <summary>
        /// An initial pass at a method to verify whether a value is
        /// kosher for SQL Server datetime
        /// </summary>
        /// <param name="someval">A date string that may parse</param>
        /// <returns>true if the parameter is valid for SQL Sever datetime</returns>
        public static bool IsValidSqlDateTime(string someval)
        {
            bool valid = false;
            DateTime testDate = DateTime.MinValue;
            DateTime minDateTime = DateTime.MaxValue;
            DateTime maxDateTime = DateTime.MinValue;

            minDateTime = new DateTime(1753, 1, 1);
            maxDateTime = new DateTime(9999, 12, 31, 23, 59, 59, 997);

            if (DateTime.TryParse(someval, out testDate))
            {
                if (testDate >= minDateTime && testDate <= maxDateTime)
                {
                    valid = true;
                }
            }

            return valid;
        }

        /// <summary>
        /// An better method to verify whether a value is
        /// kosher for SQL Server datetime. This uses the native library
        /// for checking range values
        /// </summary>
        /// <param name="someval">A date string that may parse</param>
        /// <returns>true if the parameter is valid for SQL Sever datetime</returns>
        public static bool IsValidSqlDateTimeNative(string someval)
        {
            bool valid = false;
            DateTime testDate = DateTime.MinValue;
            System.Data.SqlTypes.SqlDateTime sdt;

            if (DateTime.TryParse(someval, out testDate))
            {
                try
                {
                    // take advantage of the native conversion
                    sdt = new System.Data.SqlTypes.SqlDateTime(testDate);
                    valid = true;
                }
                catch (System.Data.SqlTypes.SqlTypeException)
                {
                    // no need to do anything, this is the expected out of range error
                }
            }

            return valid;
        }

    }
}