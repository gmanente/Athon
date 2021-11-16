using Sistema.Api.dll.Src.Comum.DAO.LogExceptionDAO;
using Sistema.Api.dll.Src.Comum.VO.LogErroSistemaVO;
using System.Data.SqlClient;

namespace Sistema.Api.dll.Src.Comum.BE.LogExceptionBE
{
    public class LogErroSistemaBE : AbstractBE
    {
        public LogErroSistemaBE()
            : base()
        {
        }

        public LogErroSistemaBE(SqlCommand sqlComm)
            : base(sqlComm)
        {
        }

        public void InserirInserirLogErro(LogErroSistemaVO logErroSistema)
        {
            LogErroSistemaDAO insLogErroSistemaDao = null;
            try
            {
                BeginTransaction();
                insLogErroSistemaDao = new LogErroSistemaDAO(GetSqlCommand());
                insLogErroSistemaDao.InserirLogErro(logErroSistema);
                GetSqlCommand().ExecuteNonQuery();
                Commit();
            }
            catch (SqlException)
            {
                Rollback();
                throw;
            }
            finally
            {

                if (insLogErroSistemaDao != null)
                {
                    insLogErroSistemaDao = null;
                }
                FecharConexao();
            }
        }

    }
}