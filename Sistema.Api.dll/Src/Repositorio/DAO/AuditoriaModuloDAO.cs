using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sistema.Api.dll.Src.Repositorio.DAO
{
    public class AuditoriaModuloDAO : AbstractDAO
    {
        public AuditoriaModuloDAO(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public void Atualizar(long idAuditoria, long idModulo)
        {
            try
            {
                objSbUpdate = new StringBuilder();
                objSbUpdate.AppendLine(@"UPDATE DBAthon.dbo.Auditoria                    ");
                objSbUpdate.AppendLine(@"  SET                                               ");
                objSbUpdate.AppendLine(@"    IdModulo = " + idModulo + "                     ");
                objSbUpdate.AppendLine(@"WHERE IdAuditoria = @IdAuditoria                    ");

                GetSqlCommand().CommandText = "";
                GetSqlCommand().CommandText = objSbUpdate.ToString();
                GetSqlCommand().Parameters.Clear();
                GetSqlCommand().Parameters.Add("IdAuditoria", SqlDbType.Int).Value = idAuditoria;
                GetSqlCommand().ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (objSbUpdate != null)
                {
                    objSbUpdate = null;
                }
            }

        }
    }
}